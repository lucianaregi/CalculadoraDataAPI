# CalculadoraDataAPI
API que calcula datas. Converte a data para o calendário Julian, calcula e converte para o calendário Gregoriano.

## Instalação

**bash** : 
git clone https://github.com/lucianaregi/CalculadoraDataAPI

## Como usar

**swagger** : 
documentação do método CalcularData: http://localhost:16394/index.html

- Adicionar data no formato dd/MM/yyyy HH:mm;
- Adicionar um operador matemático de soma (+) ou subtração (-);
- Adicionar um valor;

## Exemplo
- http://localhost:16394/api/v1/calculardata?data=14%2F03%2F2021%2019%3A12&operacao=%2B&valor=3000
- Resultado: 16/03/2021 21:11

## Licença
[MIT](https://choosealicense.com/licenses/mit/)
