namespace IP.AccCust.Api.AccountData.Integrate;

public interface IAccountAndCustomerService
{
    Task<bool> HasCustomers(CancellationToken cancellationToken);

    Task Integrate(List<AccountView> accountViews, CancellationToken cancellationToken);
}

internal sealed class AccountAndCustomerService(
    IAccIPExtUoW iPExtUoW,
    ILogger<AccountAndCustomerService> _logger
    ) : IAccountAndCustomerService
{
    private readonly IAccountIPRepository _accountRepository =
        iPExtUoW.GetRepository<IAccountIPRepository>();

    private readonly ICustomerRepository _customerRepository =
            iPExtUoW.GetRepository<ICustomerRepository>();

    private readonly IAccountSubscriptionRepository _subscriptionRepository =
        iPExtUoW.GetRepository<IAccountSubscriptionRepository>();

    public async Task<bool> HasCustomers(CancellationToken cancellationToken) =>
        await _customerRepository.Entities.AnyAsync(cancellationToken);

    public async Task Integrate(List<AccountView> accountViews, CancellationToken cancellationToken)
    {
        Dictionary<string, Customer> accountIPCustomers = [];

        IOrderedEnumerable<AccountView> customers = accountViews
            .OrderBy(x => x.PersonId)
            .ThenBy(x => x.AccountUpdatedAt);

        await IntegrateCustomers(accountIPCustomers, customers, cancellationToken);

        IOrderedEnumerable<AccountView> accounts = accountViews
            .OrderBy(x => x.AccountNumber)
            .ThenBy(x => x.AccountUpdatedAt);

        await IntegrateAccounts(accounts, accountIPCustomers, cancellationToken);
    }

    private async Task<AccountSubscription> GetSubscription(
        Dictionary<string, AccountSubscription> subscriptions,
        string planId)
    {
        if (subscriptions.TryGetValue(planId, out var accountIPSubscription)) return accountIPSubscription;

        AccountSubscription subscription =
            _subscriptionRepository.Entities.First(x => x.ExternalId == planId);

        subscriptions.Add(planId, subscription);

        return subscription;
    }

    private async Task<AccountIP> IntegrateAccountCreate(
        int accountNumber,
        AccountIPStatus accountIPStatus,
        AccountIPType accountIPType,
        AccountIPAccountSubscription accountIPSubscription,
        AccountIPCustomer accountIPCustomer)
    {
        AccountIP currentAccount = AccountIP.Create(
            accountNumber,
            accountIPCustomer,
            accountIPStatus,
            accountIPType);
        currentAccount.AddSubscription(accountIPSubscription);

        await _accountRepository.Create(currentAccount);
        return currentAccount;
    }

    private async Task IntegrateAccounts(
        IOrderedEnumerable<AccountView> accounts,
        Dictionary<string, Customer> accountIPCustomers,
        CancellationToken cancellationToken)
    {
        Dictionary<string, AccountSubscription> subscriptions = [];

        List<AccountView> filteredAccounts = [.. accounts
            .GroupBy(x => x.AccountNumber)
            .Select(x => x.Last())];

        int count = 0;
        int total = filteredAccounts.Count;

        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - Started to integrate {Count} accounts",
            nameof(AccountAndCustomerService),
            total);

        foreach (AccountView accountView in filteredAccounts)
        {
            _logger.LogInformation(
                "[{ServiceName}]:IntegrateAccount - integrate {Count}/{Total} account",
                nameof(AccountAndCustomerService),
                ++count,
                total);

            int accountNumber = accountView.AccountNumber;
            string accountUpdatedAt = accountView.AccountUpdatedAt.ToString();
            string planId = accountView.PlanId.ToString();
            string personId = accountView.PersonId.ToString();
            string accountStatus = accountView.AccountStatus;
            Customer customer = accountIPCustomers[personId];
            AccountSubscription subscription = await GetSubscription(subscriptions, planId);
            try
            {
                AccountIPStatus accountIPStatus = accountStatus switch
                {
                    "ACTIVE" => AccountIPStatus.Active,
                    "REMOVED" => AccountIPStatus.Closed,
                    _ => throw new NotImplementedException(),
                };
                AccountIPType accountIPType = AccountIPType.Transactional;

                AccountIPAccountSubscription accountIPSubscription = new(subscription.Id.Value, subscription.Name.Value);
                AccountIPCustomer accountIPCustomer = new(customer.Id.Value, customer.Name.Value);

                AccountIP? currentAccount = _accountRepository.Entities.FirstOrDefault(x => x.Number == accountNumber);

                if (currentAccount == null)
                    currentAccount = await IntegrateAccountCreate(
                        accountNumber,
                        accountIPStatus,
                        accountIPType,
                        accountIPSubscription,
                        accountIPCustomer);
                else
                    IntegrateAccountUpdate(
                        accountIPStatus,
                        accountIPSubscription,
                        accountIPCustomer,
                        currentAccount);

                await iPExtUoW.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    "[{ServiceName}]:IntegrateAccount - integrate account {AccountNumber} for date {Date} failed: {StackTrace}",
                    nameof(AccountAndCustomerService),
                    accountNumber,
                    accountUpdatedAt,
                    exception.GetInnerExceptions());
            }
        }

        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - End to integrate {Total} customers",
            nameof(AccountAndCustomerService),
            total);
    }

    private void IntegrateAccountUpdate(
        AccountIPStatus accountIPStatus,
        AccountIPAccountSubscription accountIPSubscription,
        AccountIPCustomer accountIPCustomer,
        AccountIP currentAccount)
    {
        if (currentAccount.StatusCode != accountIPStatus)
            currentAccount.SetCustomer(accountIPCustomer);

        if (currentAccount.StatusCode == accountIPStatus)
            currentAccount.SetStatusCode(accountIPStatus);

        if (currentAccount.Subscription?.Id != accountIPSubscription.Id)
            currentAccount.AddSubscription(accountIPSubscription);

        _accountRepository.Update(currentAccount);
    }

    private async Task<Customer> IntegrateCustomerCreate(
                string customerId,
        PersonTypeEnum personTypeEnum,
        CustomerStatusEnum customerStatusEnum,
        CustomerName name,
        CustomerTradingName tradeName,
        CPFOrCNPJ document)
    {
        Customer currentCustomer = Customer.Create(
            personTypeEnum,
            name,
            tradeName,
            document,
            customerStatusEnum,
            customerId);

        await _customerRepository.Create(currentCustomer);
        return currentCustomer;
    }

    private async Task IntegrateCustomers(
        Dictionary<string, Customer> accountIPCustomers,
        IOrderedEnumerable<AccountView> customers,
        CancellationToken cancellationToken)
    {
        List<AccountView> filteredCustomers = [.. customers
            .GroupBy(x => x.PersonId)
            .Select(x => x.Last())];

        int count = 0;
        int total = filteredCustomers.Count;

        _logger.LogInformation(
           "[{ServiceName}]:IntegrateAccount - Started to integrate {Count} customers",
              nameof(AccountAndCustomerService),
              total);

        foreach (var customer in filteredCustomers)
        {
            _logger.LogInformation(
                "[{ServiceName}]:IntegrateAccount - integrate {Count}/{Total} customer",
                nameof(AccountAndCustomerService),
                ++count,
                total);

            string customerId = customer.PersonId.ToString();
            string customerName = customer.PersonName;
            string personType = customer.PersonType;
            string customerStatus = customer.PersonStatus;
            string customerTradeName = customer.PersonTradeName;
            string personDocument = customer.PersonDocument;
            string personUpdatedAt = customer.PersonUpdatedAt.ToString();

            try
            {
                PersonTypeEnum personTypeEnum = personType == "NATURAL_PERSON" ? PersonTypeEnum.Individual : PersonTypeEnum.Company;
                CustomerStatusEnum customerStatusEnum = customerStatus == "ACTIVE" ? CustomerStatusEnum.Active : CustomerStatusEnum.Inactive;
                CustomerName name = new(customerName);
                CustomerTradingName tradeName = new(customerTradeName);
                CPFOrCNPJ document = new(personDocument);

                Customer? currentCustomer =
                    await _customerRepository.Entities
                    .FirstOrDefaultAsync(c => c.ExternalId == customerId, cancellationToken);

                if (currentCustomer == null)
                    currentCustomer = await IntegrateCustomerCreate(customerId, personTypeEnum, customerStatusEnum, name, tradeName, document);
                else
                    IntegrateCustomerUpdate(personTypeEnum, customerStatusEnum, name, tradeName, document, currentCustomer);

                await iPExtUoW.SaveChangesAsync(cancellationToken);

                if (!accountIPCustomers.ContainsKey(customerId))
                    accountIPCustomers.Add(customerId, currentCustomer);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    "[{ServiceName}]:IntegrateAccount - integrate customer {CustomerId} for date {Date} failed: {StackTrace}",
                    nameof(AccountAndCustomerService),
                    customerId,
                    personUpdatedAt,
                    exception.GetInnerExceptions());
            }
        }

        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - End to integrate {Total} customers",
            nameof(AccountAndCustomerService),
            total);
    }

    private void IntegrateCustomerUpdate(
        PersonTypeEnum personTypeEnum,
        CustomerStatusEnum customerStatusEnum,
        CustomerName name,
        CustomerTradingName tradeName,
        CPFOrCNPJ document,
        Customer currentCustomer)
    {
        if (currentCustomer.Name.Value != name.Value)
            currentCustomer.SetName(name);

        if (currentCustomer.TradingName.Value != tradeName.Value)
            currentCustomer.SetTradingName(tradeName);

        if (currentCustomer.DocumentNumber.Value != document.Value)
            currentCustomer.SetDocumentNumber(document);

        if (currentCustomer.StatusCode != customerStatusEnum)
            currentCustomer.SetStatus(customerStatusEnum);

        if (currentCustomer.PersonTypeCode != personTypeEnum)
            currentCustomer.SetPersonType(personTypeEnum);

        _customerRepository.Update(currentCustomer);
    }
}