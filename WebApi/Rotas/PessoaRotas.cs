using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Rotas
{
    public static class PessoaRotas
    {
        public static List<Pessoa> Pessoas = new() 
        { 
            new (Guid.NewGuid(), nome: "Gon"), 
            new (Guid.NewGuid(), nome: "Killua"),
            new (Guid.NewGuid(), nome: "Hisoka"),
        };


        public static void MapPessoaRotas(this WebApplication app)
        {
            app.MapGet("/pessoas", () =>  Pessoas);

            app.MapGet("/pessoas/{nome}", 
                (string nome) => Pessoas.Find( x => x.Nome == nome));

            app.MapPost("pessoas", (HttpContext request, Pessoa pessoa) => 
            {
                var nome = request.Request.Query["name"];

                 Pessoas.Add(pessoa);
                 return pessoa;
                
            });

            app.MapPut("/pessoas/{id:guid}", (Guid id, Pessoa pessoa) => 
            { 
                var encontrado = Pessoas.Find(x => x.Id == id);

                if (encontrado == null)
                    return Results.NotFound();

                encontrado.Nome = pessoa.Nome;

                return Results.Ok(encontrado);
            });
             
            app.MapDelete("/pessoas/{nome}", (string nome) => 
            { 
                var pessoa = Pessoas.FirstOrDefault(p => p.Nome == nome);
                Pessoas.Remove(pessoa);
            });
        }
    }
}
