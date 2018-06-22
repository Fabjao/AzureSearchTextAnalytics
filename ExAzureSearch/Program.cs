using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ExAzureSearch
{
    public class Program
    {
        static void Main(string[] args)
        {
            //https://thnetazsearch.search.windows.net
            //6E74D968A34AD1E6A47C9EEAD75ABD09
            //mbafiap

            SearchServiceClient client = new SearchServiceClient("thnetazsearch",
                new SearchCredentials("6E74D968A34AD1E6A47C9EEAD75ABD09"));
            ISearchIndexClient index = client.Indexes.GetClient("mbafiap");
            #region Enviar o documento para o azure search

            //var document = new CustomDocument
            //{
            //    id = "45240",
            //    cidade = "Ferraz de Vasconcelos",
            //    email = "rm45240@fiap.com.br",
            //    idade = 32,
            //    nome ="Fábio Gonçalves",
            //    Endereco = "Rua da alegria, 1000"
            //};
            //IndexBatch<CustomDocument> batch = IndexBatch.MergeOrUpload(new List<CustomDocument> { document });
            //index.Documents.Index(batch);
            #endregion

            #region Fazer a busca do arquivo no azure search
            Console.WriteLine("digite um termo para a busca");
            var termo = Console.ReadLine();

           var response = index.Documents.Search<CustomDocument>(termo, new SearchParameters
            {
                IncludeTotalResultCount = true,
                OrderBy = new List<string>{"nome"}
            });
            Console.WriteLine($"{response.Count} documentos encontrados");

            foreach (var item in response.Results)
            {
                Console.WriteLine($"{item.Document.nome} {item.Document.email}");
            }
            Console.Read();

            #endregion
        }
    }

    public class CustomDocument
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        public string id { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        public string nome { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        public string email { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        public int idade { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsFacetable]
        public string cidade { get; set; }

        [JsonProperty("endereco")]
        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        public string Endereco { get; set; }
    }
}
