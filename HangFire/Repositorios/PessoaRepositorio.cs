using HangFire.RN.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangFire.RN.Repositorios
{
    public class PessoaRepositorio : RepositorioDapper<Pessoa>
    {
        public Pessoa Gravar(Pessoa pessoa, out bool sucesso)
        {
            var sql = (pessoa.Id == 0) ? @"Insert into geral.Pessoa (
                            Nome,
                            Sobrenome
                        ) values (
                            @Nome,
                            @Sobrenome
                        ); select scope_identity();"
                        : @"
                        Update geral.Pessoa set 
                            Nome = @Nome,
                            Sobrenome = @Sobrenome
                        where Id = @Id;
                        select @Id;
                        ";
            sucesso = Gravar(sql, out long id, pessoa);
            if (sucesso) pessoa.Id = Convert.ToInt32(id);
            return pessoa;
        }

        public Pessoa Buscar(int id)
        {
            var sql = "Select * from geral.Pessoa where Id = @id";
            return Buscar(sql, new { id });
        }

        public List<Pessoa> Listar()
        {
            var sql = "Select * from geral.Pessoa";
            return Listar(sql).ToList();
        }

        public void Excluir(Pessoa pessoa)
        {
            var sql = "Delete geral.Pessoa where Id = @Id";
            Excluir(sql, new { pessoa.Id });
        }
    }
}
