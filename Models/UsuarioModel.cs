using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IATech.Models
{
    public class UsuarioModel
    {  
        // Variavéis que salvam os dados e transferem para o banco de dados
        [DisplayName("ID")] 
        public int UsuarioID { get; set; }

        [DisplayName("Nome")]// Altera o nome de display no formulario
        [Required(ErrorMessage = "O campo é obrigatório")]// Campo obrigatório
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O tamanho do nome deve ser maior que 4")]// Tamanho minimo e maxímo de caractéres
        public string UsuarioNome { get; set; }
        
        [DisplayName("Email")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O tamanho do nome deve ser maior que 4")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um E-mail válido.")]// Criando um padrão para cadastro de Email
        public string UsuarioEmail { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50)]
        public string UsuarioLogin { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50)]
        public string UsuarioSenha { get; set; }
        
        /*
        [DisplayName("")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50)]
        public string UsuarioHistoria { get; set; }
        */

        // Criar uma constante para conexao com o banco de dados
        readonly string connectionString = @"Data Source=LAPTOP-IEDMNFLP\SQLEXPRESS;Initial Catalog=primeiro_banco_sqlserver;Integrated Security=True";
        // Função para salvar os dados que virem do banco de dados
        
        public DataTable Listar()
        {
            // Criar uma variável para receber os dados da tabela do banco de dados
            DataTable tblUsuario = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                // Abrir a conexão com o banco de dados
                sqlCon.Open();

                // Criar uma instrução SQL para ser executada no servidor SQL Server
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM usuario", sqlCon);

                // string sql = "SELECT * FROM usuario";
                // SqlDataApdaper sqlDa = new SqlDataAdapter

                // Recuperação dos dados após a execução da instrução
                sqlDa.Fill(tblUsuario);
            }

            // Retornar os dados obtidos para serem mostrados na View (Index)
            return tblUsuario;
        }
        public void Salvar()
        {
            // Cria a conexão com o banco de dados
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                // Realiza a abertura do banco de dados
                sqlCon.Open();

                // Cria uma instrução para ser execultada pelo Sql Server
                SqlCommand sqlCmd = new SqlCommand("INSERT INTO usuario VALUES (@Nome, @Email, @Login, @Senha)", sqlCon);

                // sqlCmd.Parameters.AddWithValue("@UsuarioID", UsuarioID);
                sqlCmd.Parameters.AddWithValue("@Nome",  UsuarioNome);
                sqlCmd.Parameters.AddWithValue("@Email", UsuarioEmail);
                sqlCmd.Parameters.AddWithValue("@Login", UsuarioLogin);
                sqlCmd.Parameters.AddWithValue("@Senha", UsuarioSenha);

                // execultar o comando no Sql (Tecla F5 do Sql Server)
                sqlCmd.ExecuteNonQuery();
            }
        }
        // Método editar para selecionar o registro desejado no banco de dados
        // O parâmetro id é o identificador do registro
        public void Editar(int idUsuario)
        {
            DataTable tblUsuario = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                // É necessário utilizar a cláusula Where
                SqlDataAdapter sqlDa = new SqlDataAdapter(
                "SELECT * FROM usuario WHERE UsuarioID = @UsuarioID", sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@UsuarioID", idUsuario);

                // Recuperar o registro
                sqlDa.Fill(tblUsuario);
            }
            // Atribuir os dados retornados do banco de dados para as variáveis do Model
            UsuarioID = Convert.ToInt32(tblUsuario.Rows[0][0].ToString());
            UsuarioNome = tblUsuario.Rows[0][1].ToString();
            UsuarioEmail = tblUsuario.Rows[0][2].ToString();
            UsuarioLogin = tblUsuario.Rows[0][3].ToString();
            UsuarioSenha = tblUsuario.Rows[0][4].ToString();
        }
        public void Atualizar()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                // Criação da intrução SQL para ser executada no bamco de dados
                // A cláusula WHERE vai garantir que somente o registro será atualizado
                SqlCommand sqlCmd = new SqlCommand(
                    "UPDATE usuario SET " +
                    "UsuarioNome = @UsuarioNome, " +
                    "UsuarioEmail = @UsuarioEmail, " +
                    "UsuarioLogin = @UsuarioLogin, " +
                    "UsuarioSenha = @UsuarioSenha " +
                    "WHERE UsuarioID = @UsuarioID", sqlCon // Condição para a atualização do registro
                    );
                
                sqlCmd.Parameters.AddWithValue("@UsuarioID", UsuarioID); // Campo somente para a cláusula WHERE
                sqlCmd.Parameters.AddWithValue("@UsuarioNome", UsuarioNome);
                sqlCmd.Parameters.AddWithValue("@UsuarioEmail", UsuarioEmail);
                sqlCmd.Parameters.AddWithValue("@UsuarioLogin", UsuarioLogin);
                sqlCmd.Parameters.AddWithValue("@UsuarioSenha", UsuarioSenha);

                // Executa o comando UPDATE no banco de dados
                sqlCmd.ExecuteNonQuery();
            }
        }
        // Remove da tabela o registro indicado em idUsuario
        public void Excluir(int idUsuario)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                // Cria a instrunção SQL DELETE para remover o registro indicado com o ID
                SqlCommand sqlCmd = new SqlCommand(
                    "DELETE usuario WHERE UsuarioID = @UsuarioID", sqlCon);
                sqlCmd.Parameters.AddWithValue("@UsuarioID", idUsuario);
                sqlCmd.ExecuteNonQuery();
            }
        }
    }
}