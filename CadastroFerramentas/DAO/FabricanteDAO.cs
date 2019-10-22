using CadastroFerramentas.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroFerramentas.DAO
{
    public  class FabricanteDAO
    {
        public  List<FabricanteViewModel> ListaFabricante()
        {
            List<FabricanteViewModel> lista = new List<FabricanteViewModel>();
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemFabricante", null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaFabricante(registro));
            return lista;
        }


        private  FabricanteViewModel MontaFabricante(DataRow registro)
        {
            FabricanteViewModel c = new FabricanteViewModel()
            {
                Id = Convert.ToInt32(registro["id"]),
                Descricao = registro["descricao"].ToString()
            };
            return c;
        }

        

    }
}
