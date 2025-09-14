import { Injectable } from '@angular/core';
import { ApiService } from './api.service'; // <— usa o helper
import { HttpParams } from '@angular/common/http';

export interface Livro {
  id?: string;
  titulo: string;
  autor: string;
  anoPublicacao?: number | null;
  isbn?: string | null;
  editora?: string | null;
  dataPublicacao: string; // yyyy-MM-dd
  preco: number;
  estoque: number;
}

export interface PageResult<T> {
  total: number;
  page: number;
  pageSize: number;
  itens: T[];
}

@Injectable({ providedIn: 'root' })
export class LivrosService {
  private base = '/Livros'; // só o caminho; o ApiService prefixa com environment.apiBaseUrl

  constructor(private api: ApiService) {}

  listar(busca?: string, page = 1, pageSize = 20) {
    const params: Record<string, any> = { page, pageSize };
    if (busca) params['q'] = busca; // ajuste conforme seu backend
    return this.api.get<PageResult<Livro>>(this.base, params);
  }

  obter(id: string) {
    return this.api.get<Livro>(`${this.base}/${id}`);
  }

  criar(dto: Livro) {
    return this.api.post<Livro>(this.base, dto);
  }

  atualizar(id: string, dto: Livro) {
    return this.api.put<Livro>(`${this.base}/${id}`, dto);
  }

  excluir(id: string) {
    return this.api.delete<void>(`${this.base}/${id}`);
  }
}
// serviço p/ CRUD de livros via API REST com paginação e busca
// usa token JWT do localStorage no cabeçalho Authorization p/ criar/atualizar/excluir