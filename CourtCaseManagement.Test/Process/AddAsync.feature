#language: pt-br

Funcionalidade: Processos
	Para quanto precisar cadastrar um novo processo
	Enquanto funcionario
	Eu gostaria de cadastrar um novo processo

@ProcessAddAsync
Esquema do Cenário: Cadastrar novo processo
	Dado um funcionario cadastrando um novo processo <unifiedProcessNumber>
	E com a data de distribuição <distributionDate>
	E com o processo segredo de justiça <justiceSecret>
	E com a pasta física cliente <clientPhysicalFolder>
	E com a descrição <description>
	E com a situação <situationId>
	E com o cpf do responsável <responsiblesCpf>
	E com o nome do responsável <responsiblesName>
	E com o e-mail do responsável <responsiblesEMail>
	E com a foto do responsável <image>
	Quando solicitar o cadastro do processo
	Então o sistema retornara o código 201

Exemplos:
| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf  | responsiblesName                | responsiblesEMail   | image   |
| "3513038-00.2016.8.23.0023" | "2020-10-07"     | "S"           | "C"				    | "teste 1"   | "Em andamento" | "237.958.172-00" | "Nicolas Thales Carlos Moraes"  | "nicolas@gmail.com" | "teste" |
| "3513038-00.2016.8.23.0024" | ""               | "N"           | ""					| ""          | "Desmembrado"  | "558.526.150-99" | "Murilo Miguel Benício Ribeiro" | "murilo@gmail.com"  | "teste" |

@ProcessAddAsync
Esquema do Cenário: Cadastrar novo processo sem informar todos os campos obrigatórios
	Dado um funcionario cadastrando um novo processo <unifiedProcessNumber>
	E com a data de distribuição <distributionDate>
	E com o processo segredo de justiça <justiceSecret>
	E com a pasta física cliente <clientPhysicalFolder>
	E com a descrição <description>
	E com a situação <situationId>
	E com o cpf do responsável <responsiblesCpf>
	E com o nome do responsável <responsiblesName>
	E com o e-mail do responsável <responsiblesEMail>
	E com a foto do responsável <image>
	Quando solicitar o cadastro do processo
	Então o sistema retornara o código 400
	E apresentara os seguintes codigo de erros <messages>

Exemplos:
| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf | responsiblesName                | responsiblesEMail   | image   | messages                                                                                      |
| "3513038-00.2016.8.23.0023" | "2020-10-07"     | "S"           | "C"                  | "teste 1"   | "Em andamento" | ""              | "Nicolas Thales Carlos Moraes"  | "nicolas@gmail.com" | "teste" | "RequiredResponsibles"                                                                        |
| ""                          | ""               | "N"           | ""                   | ""          | "Desmembrado"  | ""              | "Murilo Miguel Benício Ribeiro" | ""                  | "teste" | "RequiredResponsibles;RequiredUnifiedProcessNumber"                                           |
| ""                          | ""               | ""            | ""                   | ""          | "Desmembrado"  | ""              | "Murilo Miguel Benício Ribeiro" | ""                  | "teste" | "RequiredResponsibles;RequiredUnifiedProcessNumber;RequiredJusticeSecret"                     |
| ""                          | ""               | ""            | ""                   | ""          | ""             | ""              | "Murilo Miguel Benício Ribeiro" | ""                  | "teste" | "RequiredResponsibles;RequiredUnifiedProcessNumber;RequiredJusticeSecret;RequiredSituationId" |
