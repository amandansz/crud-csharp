# Documentacao Tecnica das Alteracoes de Validacao e Robustez

## Objetivo

Este documento explica as alteracoes feitas no fluxo de cadastro de produtos para tornar o backend mais seguro, previsivel e alinhado com boas praticas de C#.

Escopo das alteracoes:

- Entrada de preco no console
- Tratamento de erro de validacao
- Regras de negocio no servico
- Encapsulamento da colecao interna

## Visao Geral: antes x depois

### Fluxo antigo

1. O metodo de cadastro lia nome e preco diretamente no console.
2. O preco era convertido com parse direto.
3. Em caso de texto invalido para preco, a aplicacao quebrava com excecao.
4. O servico aceitava nome vazio e preco negativo.
5. A lista interna era devolvida diretamente para quem chamasse.

### Fluxo atual

1. O preco e lido por um metodo dedicado que so retorna quando a entrada e valida.
2. O fluxo de cadastro captura erro de validacao de negocio e exibe mensagem amigavel.
3. O servico valida nome e preco antes de criar o produto.
4. O servico retorna uma copia da lista, protegendo estado interno.

---

## Program.cs

Referencia: [Program.cs](../Program.cs)

### Novo metodo: LerPrecoValido

Local: [Program.cs](../Program.cs#L85)

O que faz:

- Le o valor digitado para preco em loop.
- Tenta converter usando a cultura atual da maquina.
- Se falhar, tenta converter com cultura invariavel.
- So sai do loop quando encontrar um numero valido.

Por que isso e melhor que o antigo:

- Antes era usado parse direto, que lanca excecao em entrada invalida.
- Agora usa TryParse, que evita quebrar a aplicacao por erro de digitacao.
- O metodo tambem melhora a experiencia em ambientes com diferenca entre virgula e ponto decimal.

Conceito backend envolvido:

- Robustez de entrada.
- Tolerancia a variacoes de ambiente.

### Metodo alterado: CriarProduto

Local: [Program.cs](../Program.cs#L67)

Antes:

- Fazia parse direto do preco.
- Chamava o servico sem tratar erro de validacao.

Depois:

- Delega leitura do preco para LerPrecoValido.
- Chama o servico dentro de try/catch de ArgumentException.
- Exibe mensagem de erro clara para o usuario quando a regra de negocio reprova os dados.

Conceito backend envolvido:

- Separacao de responsabilidades.
- Tratamento controlado de excecoes esperadas.

---

## Services/ProdutoService.cs

Referencia: [Services/ProdutoService.cs](../Services/ProdutoService.cs)

### Campo alterado: produtos

Local: [Services/ProdutoService.cs](../Services/ProdutoService.cs#L7)

Antes:

- A lista nao era readonly.

Depois:

- A referencia da lista virou readonly.

Resultado pratico:

- O codigo deixa explicito que a colecao existe durante toda a vida do servico.
- Reduz risco de reatribuicao acidental da lista.

### Metodo alterado: Criar

Local: [Services/ProdutoService.cs](../Services/ProdutoService.cs#L10)

Antes:

- Criava produto sem validar nome.
- Aceitava preco negativo.
- Nao normalizava o nome.

Depois:

- Valida nome com IsNullOrWhiteSpace.
- Rejeita preco negativo.
- Faz trim no nome antes de salvar.
- Lanca ArgumentException com mensagem de negocio.

Por que isso e importante:

- Regras de negocio devem ficar na camada de servico, nao depender apenas da interface de entrada.
- Se no futuro houver API HTTP, fila ou outro tipo de entrada, as mesmas regras continuam valendo.

Conceito backend envolvido:

- Camada de dominio/servico como guardiao da consistencia.
- Validacao centralizada.

### Metodo alterado: ListarProdutos

Local: [Services/ProdutoService.cs](../Services/ProdutoService.cs#L32)

Antes:

- Retornava a lista interna diretamente.

Depois:

- Retorna uma nova lista com os mesmos itens.

Por que isso e melhor:

- Evita que codigo externo modifique a colecao interna do servico sem passar pelas regras.
- Melhora encapsulamento e previsibilidade de estado.

Conceito backend envolvido:

- Encapsulamento defensivo.

---

## Mapa rapido de aprendizado

1. TryParse versus Parse: quando usar cada um.
2. Excecoes de validacao (ArgumentException) e onde captura-las.
3. Regras de negocio no servico versus validacao na interface.
4. Encapsulamento de colecoes em classes de dominio/servico.
5. Normalizacao de entrada (trim, formato decimal, cultura).

## Proximo passo recomendado

Aplicar a mesma linha de qualidade para persistencia em banco:

1. Criar repositorio para Produto com comandos SQL parametrizados.
2. Mover leitura e escrita para MySQL mantendo as validacoes no servico.
3. Adicionar testes unitarios para o servico (nome vazio, preco negativo, trim).
