namespace IP.Core.Persistence.Seeders;

internal class StateData
{
    private static List<string> StatesInitial { get; set; } = [
        "11,Rondônia,RO",
        "12,Acre,AC",
        "13,Amazonas,AM",
        "14,Roraima,RR",
        "15,Pará,PA",
        "16,Amapá,AP",
        "17,Tocantins,TO",
        "21,Maranhão,MA",
        "22,Piauí,PI",
        "23,Ceará,CE",
        "24,Rio Grande do Norte,RN",
        "25,Paraíba,PB",
        "26,Pernambuco,PE",
        "27,Alagoas,AL",
        "28,Sergipe,SE",
        "29,Bahia,BA",
        "31,Minas Gerais, MG",
        "32,Espírito Santo, ES",
        "33,Rio de Janeiro,RJ",
        "35,São Paulo, SP",
        "41,Paraná,PR",
        "42,Santa Catarina, SC",
        "43,Rio Grande do Sul,RS",
        "50,Mato Grosso do Sul,MS",
        "51,Mato Grosso, MT",
        "52,Goiás,GO",
        "53,Distrito Federal, DF",
        ];

    public static Dictionary<string, Guid> CodeGuide { get; set; } = new() {
        { "11", Guid.Parse("019ad66a-9273-73e9-9837-86760b4b8c84") },
        { "14", Guid.Parse("019ad66a-92cc-7202-8a3b-44b66bb02bf2") },
        { "12", Guid.Parse("019ad66a-92cc-723e-95e1-2bdc0dbb299b") },
        { "13", Guid.Parse("019ad66a-92cc-749f-a952-57908a53d3aa") },
        { "16", Guid.Parse("019ad66a-92cd-7317-ab47-c3f3b41a9d01") },
        { "15", Guid.Parse("019ad66a-92cd-75d5-ba46-c3d3b79995ef") },
        { "17", Guid.Parse("019ad66a-92cd-7d1e-8eb1-d83d52e5af7d") },
        { "22", Guid.Parse("019ad66a-92cd-7e5c-8f2d-1619eab7cff1") },
        { "23", Guid.Parse("019ad66a-92cd-7efc-b8a2-f991d68e8600") },
        { "21", Guid.Parse("019ad66a-92cd-7f29-a4c1-fdaaeefc9848") },
        { "25", Guid.Parse("019ad66a-92ce-73fc-bb01-1f191f62bbb7") },
        { "24", Guid.Parse("019ad66a-92ce-747a-bbf3-7bf25bff82e5") },
        { "27", Guid.Parse("019ad66a-92ce-7694-894f-377a12440b41") },
        { "28", Guid.Parse("019ad66a-92ce-797a-81c4-711f1d0214ef") },
        { "29", Guid.Parse("019ad66a-92ce-79f9-a460-001a48e24594") },
        { "31", Guid.Parse("019ad66a-92ce-7ac4-88cc-6a28a86ad5c9") },
        { "26", Guid.Parse("019ad66a-92ce-7c8e-a313-af76d9b10159") },
        { "41", Guid.Parse("019ad66a-92cf-7103-965f-a662f918a2b7") },
        { "35", Guid.Parse("019ad66a-92cf-7744-ae40-62c48c027819") },
        { "42", Guid.Parse("019ad66a-92cf-780b-8933-8e612e2ed422") },
        { "32", Guid.Parse("019ad66a-92cf-7a79-96ec-0ac2e20eba2a") },
        { "33", Guid.Parse("019ad66a-92cf-7c8f-9596-ac62d9177659") },
        { "52", Guid.Parse("019ad66a-92d0-7168-8a72-885474828172") },
        { "53", Guid.Parse("019ad66a-92d0-7390-9979-52644eda2a67") },
        { "50", Guid.Parse("019ad66a-92d0-7803-8a73-4330308d8169") },
        { "43", Guid.Parse("019ad66a-92d0-78af-a793-fd55892f1b0f") },
        { "51", Guid.Parse("019ad66a-92d0-7ac2-a4aa-c5af60e4298c") },
        };

    public static IEnumerable<State> Entities()
    {
        foreach (var stateInitial in StatesInitial)
        {
            var parts = stateInitial.Split(',');
            var ibgeCode = parts[0].Trim();
            var name = parts[1].Trim();
            var code = parts[2].Trim();
            yield return State.CreateToSeed(CodeGuide[ibgeCode], ibgeCode, code, name);
        }
    }
}