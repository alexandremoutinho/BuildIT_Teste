using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMWeb.Models
{
    public class ConsultaCedulas
    {

        public static int SaldoCedulas()
        {
            List<Notas> notas = new List<Notas>();
            int cedulas = 0;
            foreach (var nt in notas)
            {
                cedulas += nt.Qtde;

            }
            return cedulas;
        }

        public static void AtualizarQtde(int id, int qtde)
        {
            SqlConnection dbconexao = new SqlConnection();
            dbconexao.ConnectionString = "Data Source=.\\Data\\ATM.db";
            dbconexao.OpenAsync();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = $"Update Notas set Qtde={qtde} where Notas.Notasid={id}";
            dbconexao.CloseAsync();         
        }


        public static string ConxaoStr()
        {
            string ConnectionString = "Data Source=.\\Data\\ATM.db";
            return ConnectionString;


        }
    }
}
