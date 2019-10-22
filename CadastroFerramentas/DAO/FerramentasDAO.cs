using CadastroFerramentas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CadastroFerramentas.DAO
{
    public class FerramentasDAO
    {
        public void Inserir(FerramentasViewModel ferramentas)
        {
            HelperDAO.ExecutaProc("spIncluiFerramenta",CriaParametros(ferramentas));

        }


        public void Alterar(FerramentasViewModel ferramentas)
        {
            HelperDAO.ExecutaProc("spAlteraFerramenta", CriaParametros(ferramentas));
        }



        private SqlParameter[] CriaParametros(FerramentasViewModel ferramentas)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("id", ferramentas.Id);
            parametros[1] = new SqlParameter("descricao", ferramentas.Descricao);
            parametros[2] = new SqlParameter("fabricanteId", ferramentas.FabricanteId);
            return parametros;
        }


        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id",id)
            };
          HelperDAO.ExecutaProc("spExcluiFerramenta", p);
        }

        private FerramentasViewModel MontaFerramenta(DataRow registro)
        {
            FerramentasViewModel f = new FerramentasViewModel();
            f.Id = Convert.ToInt32(registro["id"]);
            f.Descricao = registro["descricao"].ToString();
            f.FabricanteId = Convert.ToInt32(registro["fabricanteId"]);            
            return f;
        }

        public FerramentasViewModel Consulta(int id)
        {
            //string sql = "select * from ferramentas where id = " + id;


            var p = new SqlParameter[]
           {
                new SqlParameter("id",id)
           };


            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaFerramenta", p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaFerramenta(tabela.Rows[0]);
        }


        public bool ConsultaFabricante (int id)
        {
            var p = new SqlParameter[]
         {
                new SqlParameter("id",id)
         };


            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaFabricante", p);

            if (tabela.Rows.Count == 0)
                return false;
            else
                return true;
        }


        public List<FerramentasViewModel> Listagem()
        {
            List<FerramentasViewModel> lista = new List<FerramentasViewModel>();

           // string sql = "select * from ferramentas order by descricao";
            DataTable tabela = HelperDAO.ExecutaProcSelect("spListagemFerramentas", null);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaFerramenta(registro));
            return lista;
        }

        public int ProximoId()
        {
            string sql = "select isnull(max(id) +1, 1) as 'MAIOR' from ferramentas";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }

    }
}
