import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import App from './App.tsx';
import './index.css';
import {ApolloProvider} from "@apollo/client";
import client from '../../apollo-client.ts';


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <ApolloProvider client={client}>
    <BrowserRouter>
        <App />
    </BrowserRouter>
  </ApolloProvider>
);