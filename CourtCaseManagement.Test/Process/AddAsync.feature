#language: pt-br

Funcionalidade: Processos
	Para quanto precisar cadastrar um novo processo
	Enquanto funcionario
	Eu gostaria de cadastrar um novo processo

@AddAsync
Esquema do Cenário: Cadastra novo processo
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
	Quando solicitar o cancelamento do boleto
	Então o sistema retornara o boleto na situação cancelado

Exemplos:
| unifiedProcessNumber        | distributionDate | justiceSecret | clientPhysicalFolder | description | situationId    | responsiblesCpf  | responsiblesName                | responsiblesEMail   | image   |
| "3513038-00.2016.8.23.0023" | "2020-10-07"     | "S"           | "C"				    | "teste 1"   | "Em andamento" | "237.958.172-00" | "Nicolas Thales Carlos Moraes"  | "nicolas@gmail.com" | "teste" |
| "3513038-00.2016.8.23.0024" | ""               | "N"           | ""					| ""          | "Desmembrado"  | "558.526.150-99" | "Murilo Miguel Benício Ribeiro" | "murilo@gmail.com"  | "teste" |
