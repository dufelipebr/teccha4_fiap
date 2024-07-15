# teccha4_fiap
repositorio do projeto para apresentação teccha4 fiap

Projeto consiste em : 

apibronco.net -> C# API codigo com controlers utilizados e logica de persistência no atlas mongodb cloud. 
está publicada no Azure : https://dev-azure-brazilsouth-broncoapi.azurewebsites.net/swagger/index.html
* Pode existir uma limitação para rodar local porque no MongoDb do Atlas é necessário configurar o Client IP que está executando, senão bloqueia.

apibronco.xunit -> possui os testes XUNIT realizados na fase 4 

bronco_corretor_online (new)  -> React & Next.JS com a implementação de criação de proposta de seguros. 
Para buildar é necessário instalar ultima versão do node e npm e next.Js. 
Estou rodando node 10.4.0 e npm 20.11.0. Next JS 14.2.4

Para instalar o Next.js, primeiro você precisa instalar Node.js e npm no seu computador. Após isso, você pode criar um novo projeto Next.js usando o comando create-next-app. Aqui estão os passos detalhados:

1. Instale Node.js e npm. Você pode baixar ambos do site oficial do Node.js. 
**Estou utilizando a versão 20.11.0 (em 29/06/24)**

duvidas : 

[https://www.alura.com.br/artigos/como-instalar-node-js-windows-linux-macos?utm_term=&utm_campaign=[Search]+[Performance]+-+Dynamic+Search+Ads+-+Artigos+e+Conteúdos&utm_source=adwords&utm_medium=ppc&hsa_acc=7964138385&hsa_cam=11384329873&hsa_grp=169611649651&hsa_ad=703829337057&hsa_src=g&hsa_tgt=aud-574826424850:dsa-2276348409543&hsa_kw=&hsa_mt=&hsa_net=adwords&hsa_ver=3&gad_source=1&gclid=CjwKCAjw4f6zBhBVEiwATEHFVj0dfn7csm-p4q78voRPTvVtulCj81KAR9crYCKux-47l8u4O0seJBoCPfUQAvD_BwE](https://www.alura.com.br/artigos/como-instalar-node-js-windows-linux-macos?utm_term=&utm_campaign=%5BSearch%5D+%5BPerformance%5D+-+Dynamic+Search+Ads+-+Artigos+e+Conte%C3%BAdos&utm_source=adwords&utm_medium=ppc&hsa_acc=7964138385&hsa_cam=11384329873&hsa_grp=169611649651&hsa_ad=703829337057&hsa_src=g&hsa_tgt=aud-574826424850:dsa-2276348409543&hsa_kw=&hsa_mt=&hsa_net=adwords&hsa_ver=3&gad_source=1&gclid=CjwKCAjw4f6zBhBVEiwATEHFVj0dfn7csm-p4q78voRPTvVtulCj81KAR9crYCKux-47l8u4O0seJBoCPfUQAvD_BwE)

1. Abra o terminal e digite o seguinte comando para criar um novo projeto Next.js:

```
npx create-next-app@latest nome-do-seu-projeto
-- ou para instalar somente o next e react no projeto já criado
npm install next@latest react@latest react-dom@latest
```

1. Navegue para a pasta do projeto usando o comando `cd nome-do-seu-projeto`
2. Inicie o servidor de desenvolvimento com `npm run dev`

Pronto, agora você tem um novo projeto Next.js rodando em seu computador!

Para rodar em modo DEV 

```
npm run dev 
```

Instalação de bibliotecas (dependências) utilizadas no projeto

```jsx
npm install @tailwindcss/forms
npm install @heroicons/react
```



