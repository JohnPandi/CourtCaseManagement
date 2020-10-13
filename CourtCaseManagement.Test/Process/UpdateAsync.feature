#language: pt-br

Funcionalidade: Atualizar Processos
	Para quanto precisar atualizar um processo
	Enquanto funcionario
	Eu gostaria de atualizar os dados de um processo

@ProcessUpdateAsync
Esquema do Cenário: Atualizar um processo
	Dado o processo previamente cadastrado
	| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf  | responsiblesName                | responsiblesEMail   | image   |
	| "3513038-00.2016.8.23.0023" | "2020-10-07"     | "S"           | "C"                  | "teste 1"   | "Em andamento" | "237.958.172-00" | "Nicolas Thales Carlos Moraes"  | "nicolas@gmail.com" | "teste" |
	E atualizando o número do processo unificado <unifiedProcessNumber>
	E atualizando a data de distribuição para <distributionDate>
	E atualizando o segredo de justiça para <justiceSecret>
	E atualizando a pasta física cliente para <clientPhysicalFolder>
	E atualizando a descrição para <description>
	E atualizando a situação para <situationId>
	E atualizando o responsavel <responsiblesCpf> <responsiblesName> <responsiblesEMail> <image>
	Quando solicitar o atualizar um processo
	Então o sistema retornara o código 200

Exemplos:
| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf  | responsiblesName               | responsiblesEMail   | image   |
| "3513038-00.2016.8.23.0024" | "2020-09-17"     | "N"           | "D"                  | "teste 2"   | "Em andamento" | "303.951.780-53" | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" |

@ProcessUpdateAsync
Esquema do Cenário: Atualizar um processo sem informar todos os campos obrigatórios
	Dado o processo previamente cadastrado
	| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf  | responsiblesName                | responsiblesEMail   | image   |
	| "3513038-00.2016.8.23.0023" | "2020-10-07"     | "S"           | "C"                  | "teste 1"   | "Em andamento" | "237.958.172-00" | "Nicolas Thales Carlos Moraes"  | "nicolas@gmail.com" | "teste" |
	E atualizando o número do processo unificado <unifiedProcessNumber>
	E atualizando a data de distribuição para <distributionDate>
	E atualizando o segredo de justiça para <justiceSecret>
	E atualizando a pasta física cliente para <clientPhysicalFolder>
	E atualizando a descrição para <description>
	E atualizando a situação para <situationId>
	E atualizando o responsavel <responsiblesCpf> <responsiblesName> <responsiblesEMail> <image>
	Quando solicitar o atualizar um processo
	Então o sistema retornara o código 400
	E apresentara os seguintes codigo de erros <messages>

Exemplos:
| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf | responsiblesName               | responsiblesEMail   | image   | messages                                                                                      |
| "3513038-00.2016.8.23.0023" | "2020-10-07"     | "S"           | "C"                  | "teste 1"   | "Em andamento" | ""              | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "RequiredResponsibles"                                                                        |
| ""                          | ""               | "N"           | ""                   | ""          | "Desmembrado"  | ""              | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "RequiredResponsibles;RequiredUnifiedProcessNumber"                                           |
| ""                          | ""               | ""            | ""                   | ""          | "Desmembrado"  | ""              | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "RequiredResponsibles;RequiredUnifiedProcessNumber;RequiredJusticeSecret"                     |
| ""                          | ""               | ""            | ""                   | ""          | ""             | ""              | "Nicolas Thales Carlos Moraes" | "nicolas@gmail.com" | "teste" | "RequiredResponsibles;RequiredUnifiedProcessNumber;RequiredJusticeSecret;RequiredSituationId" |
