# api

Estrutura de Projeto 
- IDI: Modulo de Identidade
  - Criado com o proposito de gerenciar as funcionalidades de autenticação e autorização do sistema.
 Add-Migration V1 -Context IDIDbContext
	
- Core: Modulo de Núcleo
  - Criado com o proposito de gerenciar as funcionalidades centrais do sistema.
  Add-Migration V1 -Context CoreDbContext
	
- Com: Modulo de Comunicação
  - Criado com o proposito de gerenciar as funcionalidades de comunicação(Emails/Notificaçoes) do sistema.  
 Add-Migration V1 -Context ComDbContext
	
- AccIPInfo: Modulo de Informaçoes da conta
  - Criado com o proposito de gerenciar as informações das contas dos clientes.  
 Add-Migration V1 -Context AccIPDbContext