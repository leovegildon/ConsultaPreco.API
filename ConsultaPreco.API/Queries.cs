using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ConsultaPreco.API
{
    public class Queries
    {
             public string centro,
                           material,
                           descricao,
                           precoRegular,
                           precoMinhaLe;
        public void ConsultaPreco(string centroConsulta, string codigoProduto)
        {

            OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION="
                                                        + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.11.0.30)(PORT=1521)))"
                                                        + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=INTEGRACAOSP)));"
                                                        + "User Id=pdvuser;Password=pdv1234");
            conn.Open();
            OracleCommand oCmd = new OracleCommand();
            oCmd.Connection = conn;
            string query = "select v.centro, " +
       "v.material, " +
       "v.descricao, " +
       "v.precoRegular, " +
       "(select p.tfid_preco_venda " +
  "from tfid_promocao p " +
 "where p.tfid_unidade_fk_pk = " +
       "(select s.loja_proton_uk from tloja_sap s where s.loja_sap_pk = " + centroConsulta + ") " +
   "and p.tfid_codigo_pri_fk_pk = " +
       "(select m.tmer_codigo_pri_pk " +
          "from tmer_mercadoria m join tmer_codigo_barras b " +
          "on m.tmer_codigo_pri_pk = b.tmer_codigo_pri_fk_pk " +
          "and m.tmer_codigo_sec_pk = b.tmer_codigo_sec_fk_pk " +
         "where m.tmer_codigo_barras_ukn = " + codigoProduto + "  " +
         "or b.tmer_codigo_barras_alter_pk = " + codigoProduto + " ) " +
   "and p.tfid_codigo_sec_fk_pk = 0 " +
   "and TRUNC(sysdate) between p.tfid_data_inicio and p.tfid_data_fim " +
   "and p.tfid_tipo_preco = 1) precoMinhaLe " +
  "from(select s.loja_sap_pk as centro, " +
               "m.tmer_codigo_barras_ukn as material, " +
               "m.tmer_nome as descricao, " +
               "e.tmer_preco_venda as precoRegular " +
          "from tmer_mercadoria m " +
          "join tmer_estoque e " +
            "on m.tmer_codigo_pri_pk = e.tmer_codigo_pri_fk_pk " +
           "and m.tmer_codigo_sec_pk = e.tmer_codigo_sec_fk_pk " +
           "and e.tmer_unidade_fk_pk = " +
               "(select s.loja_proton_uk " +
                  "from tloja_sap s " +
                "where s.loja_sap_pk = " + centroConsulta + ") " +
          "join tloja_sap s " +
            "on e.tmer_unidade_fk_pk = s.loja_proton_uk " +
         "where m.tmer_codigo_barras_ukn = " + codigoProduto + " " +
            "or m.tmer_codigo_barras_ukn = " +
               "(select c.tmer_codigo_barras_ean_fkn " +
                  "from tmer_codigo_barras c " +
                 "where c.tmer_codigo_barras_alter_pk = " + codigoProduto + ") " +
           "and s.loja_sap_pk = " + centroConsulta + ") v";
            oCmd.CommandText = query;
            OracleDataReader ler = oCmd.ExecuteReader();
            while (ler.Read())
            {

                centro = ler.GetValue(0).ToString();
                material = ler.GetValue(1).ToString();
                descricao = ler.GetValue(2).ToString();
                precoRegular = ler.GetValue(3).ToString();
                precoMinhaLe = ler.GetValue(4).ToString();
                
            }

        
        }
    }
}
