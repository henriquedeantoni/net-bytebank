using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using CursoWindowsFormsBiblioteca.Databases;

namespace CursoWindowsFormsBiblioteca.Classes
{
    public class Cliente
    {
        public class Unit
        {
            [Required(ErrorMessage = "Código do Cliente é obrigatório.")]
            [RegularExpression("([0-9]+)", ErrorMessage = "Código do Cliente somente aceita valores numéricos.")]
            [StringLength(6, MinimumLength = 6, ErrorMessage = "Código do Cliente deve ter 6 dígitos.")]
            public string Id { get; set; }

            [Required(ErrorMessage = "Nome do Cliente é obrigatório.")]
            [StringLength(50, ErrorMessage = "Nome do Cliente deve ter no máximo 50 caracteres.")]
            public string Nome { get; set; }

            [StringLength(50, ErrorMessage = "Nome do Pai deve ter no máximo 50 caracteres.")]
            public string NomePai { get; set; }

            [Required(ErrorMessage = "Nome da Mãe é obrigatório.")]
            [StringLength(50, ErrorMessage = "Nome da Mãe deve ter no máximo 50 caracteres.")]
            public string NomeMae { get; set; }

            public bool NaoTemPai { get; set; }

            [Required(ErrorMessage = "CPF obrigatório.")]
            [RegularExpression("([0-9]+)", ErrorMessage = "CPF somente aceita valores numéricos.")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos.")]
            public string Cpf { get; set; }

            [Required(ErrorMessage = "Genero obrigatório.")]
            public int Genero { get; set; }

            [Required(ErrorMessage = "CEP obrigatório.")]
            [RegularExpression("([0-9]+)", ErrorMessage = "CPF somente aceita valores numéricos.")]
            [StringLength(8, MinimumLength = 8, ErrorMessage = "CPF deve ter 8 dígitos.")]
            public string Cep { get; set; }

            [Required(ErrorMessage = "Logradouro é obrigatório.")]
            [StringLength(100, ErrorMessage = "Logradouro deve ter no máximo 100 caracteres.")]
            public string Logradouro { get; set; }

            [Required(ErrorMessage = "Complemento é obrigatório.")]
            [StringLength(100, ErrorMessage = "Complemento deve ter no máximo 100 caracteres.")]
            public string Complemento { get; set; }

            [Required(ErrorMessage = "Bairro é obrigatório.")]
            [StringLength(50, ErrorMessage = "Bairro deve ter no máximo 50 caracteres.")]
            public string Bairro { get; set; }

            [Required(ErrorMessage = "Cidade é obrigatória.")]
            [StringLength(50, ErrorMessage = "Cidade deve ter no máximo 50 caracteres.")]
            public string Cidade { get; set; }

            [Required(ErrorMessage = "Estado é obrigatório.")]
            [StringLength(50, ErrorMessage = "Estado deve ter no máximo 50 caracteres.")]
            public string Estado { get; set; }

            [Required(ErrorMessage = "Número do telefone é obrigatório.")]
            [RegularExpression("([0-9]+)", ErrorMessage = "Número do telefone somente aceita valores numéricos.")]
            public string Telefone { get; set; }

            public string Profissao { get; set; }

            [Required(ErrorMessage = "Renda familiar é obrigatória.")]
            [Range(0, double.MaxValue, ErrorMessage = "Renda familiar deve ser um valor positivo.")]
            public Double RendaFamiliar { get; set; }

            public void ValidaClasse()
            {
                ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
                List<ValidationResult> results = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(this, context, results, true);

                if (isValid == false)
                {
                    StringBuilder sbrErrors = new StringBuilder();
                    foreach (var validationResult in results)
                    {
                        sbrErrors.AppendLine(validationResult.ErrorMessage);
                    }
                    throw new ValidationException(sbrErrors.ToString());
                }
            }

            public void ValidaComplemento()
            {
                if (this.NomePai == this.NomeMae)
                {
                    throw new Exception("Nome do Pai e da Mãe não podem ser iguais.");
                }
                if (this.NaoTemPai == false)
                {
                    if (this.NomePai == "")
                    {
                        throw new Exception("Nome do Pai não pode estar vazio quando a propriedade Pai Desconhecido não estiver marcada.");
                    }
                }
                bool validaCPF = Cls_Uteis.Valida(this.Cpf);
                if (validaCPF == false)
                {
                    throw new Exception("CPF inválido.");
                }
            }

            #region CRUD do Fichario

            public void IncluirFichario(string Conexao)
            {

                string clienteJson = Cliente.SerializedClassUnit(this);
                Fichario F = new Fichario(Conexao);
                if (F.status)
                {
                    F.Incluir(this.Id, clienteJson);
                    if (!F.status)
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public Unit BuscarFichario(string Id, string conexao)
            {
                Fichario F = new Fichario(conexao);
                if (F.status)
                {
                    string clienteJson = F.Buscar(Id);
                    return Cliente.DesSerializedClassUnit(clienteJson);
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public void AlterarFichario (string Conexao)
            {
                Fichario F = new Fichario(Conexao);
                if (F.status)
                {
                    string clienteJson = Cliente.SerializedClassUnit(this);
                    F.Alterar(this.Id, clienteJson);
                    if (!F.status)
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    
                    throw new Exception(F.mensagem);
                }
            }

            public void ApagarFichario(string Conexao)
            {
                Fichario F = new Fichario(Conexao);
                if (F.status)
                {
                    F.Apagar(this.Id);
                    if (!F.status)
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {

                    throw new Exception(F.mensagem);
                }
            }

            public List<string> ListaFichario(string conexao)
            {
                Fichario F = new Fichario(conexao);
                if (F.status)
                {
                    List<string> todosJson = F.BuscarTodos();
                    return todosJson;
                }
                else
                {

                    throw new Exception(F.mensagem);
                }

            }

            #endregion

            #region CRUD do Fichario Local DB

            public void IncluirFicharioDB(string Conexao)
            {

                string clienteJson = Cliente.SerializedClassUnit(this);
                FicharioDB F = new FicharioDB(Conexao);
                if (F.status)
                {
                    F.Incluir(this.Id, clienteJson);
                    if (!F.status)
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public Unit BuscarFicharioDB(string Id, string conexao)
            {
                FicharioDB F = new FicharioDB(conexao);
                if (F.status)
                {
                    string clienteJson = F.Buscar(Id);
                    return Cliente.DesSerializedClassUnit(clienteJson);
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public void AlterarFicharioDB(string Conexao)
            {
                FicharioDB F = new FicharioDB(Conexao);
                if (F.status)
                {
                    string clienteJson = Cliente.SerializedClassUnit(this);
                    F.Alterar(this.Id, clienteJson);
                    if (!F.status)
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {

                    throw new Exception(F.mensagem);
                }
            }

            public void ApagarFicharioDB(string Conexao)
            {
                FicharioDB F = new FicharioDB(Conexao);
                if (F.status)
                {
                    F.Apagar(this.Id);
                    if (!F.status)
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {

                    throw new Exception(F.mensagem);
                }
            }

            public List<List<string>> BuscarFicharioDBTodosDB(string conexao)
            {
                FicharioDB F = new FicharioDB(conexao);
                if (F.status)
                {
                    List<string> List = new List<string>();
                    List = F.BuscarTodos();
                    if(F.status)
                    {
                        List<List<string>> ListaBusca = new List<List<string>>();
                        for(int i = 0; i < ListaBusca.Count-1; i++)
                        {
                            Cliente.Unit C = Cliente.DesSerializedClassUnit(List[i]);
                            ListaBusca.Add(new List<string> { C.Id, C.Nome });
                        }
                        return ListaBusca;
                    }
                    else
                    {

                        throw new Exception(F.mensagem);
                    }
                }
                else
                {

                    throw new Exception(F.mensagem);
                }

            }

            public List<string> ListaFicharioDB(string conexao)
            {
                FicharioDB F = new FicharioDB(conexao);
                if (F.status)
                {
                    List<string> todosJson = F.BuscarTodos();
                    return todosJson;
                }
                else
                {

                    throw new Exception(F.mensagem);
                }

            }

            #endregion

            #region CRUD do Fichario SQL SERVER

            public void IncluirFicharioSQL(string Conexao)
            {
                string clienteJson = Cliente.SerializedClassUnit(this);
                FicharioSQLServer F = new FicharioSQLServer(Conexao);
                if (F.status)
                {
                    F.Incluir(this.Id, clienteJson);
                    if (!(F.status))
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public Unit BuscarFicharioSQL(string id, string conexao)
            {
                FicharioSQLServer F = new FicharioSQLServer(conexao);
                if (F.status)
                {
                    string clienteJson = F.Buscar(id);
                    return Cliente.DesSerializedClassUnit(clienteJson);
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public void AlterarFicharioSQL(string conexao)
            {
                string clienteJson = Cliente.SerializedClassUnit(this);
                FicharioSQLServer F = new FicharioSQLServer(conexao);
                if (F.status)
                {
                    F.Alterar(this.Id, clienteJson);
                    if (!(F.status))
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public void ApagarFicharioSQL(string conexao)
            {
                FicharioSQLServer F = new FicharioSQLServer(conexao);
                if (F.status)
                {
                    F.Apagar(this.Id);
                    if (!(F.status))
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }

            public List<List<string>> BuscarFicharioDBTodosSQL(string conexao)
            {
                FicharioSQLServer F = new FicharioSQLServer(conexao);
                if (F.status)
                {
                    List<string> List = new List<string>();
                    List = F.BuscarTodos();
                    if (F.status)
                    {
                        List<List<string>> ListaBusca = new List<List<string>>();
                        for (int i = 0; i <= List.Count - 1; i++)
                        {
                            Cliente.Unit C = Cliente.DesSerializedClassUnit(List[i]);
                            ListaBusca.Add(new List<string> { C.Id, C.Nome });
                        }
                        return ListaBusca;
                    }
                    else
                    {
                        throw new Exception(F.mensagem);
                    }
                }
                else
                {
                    throw new Exception(F.mensagem);
                }
            }


            #endregion

            #region "CRUD do Fichario DB SQL SERVER Relacional"

            #region "Funções Auxiliares"

            public string ToInsert()
            {
                string SQL;

                SQL = @"INSERT INTO TB_Cliente
                        (Id,
                        Nome,
                        NomePai,
                        NomeMae,
                        NaoTemPai,
                        Cpf,
                        Genero,
                        Cep,
                        Logradouro,
                        Complemento,
                        Bairro,
                        Cidade,
                        Estado,
                        Telefone,
                        Profissao,
                        RendaFamiliar) 
                        VALUES";

                SQL += "('" + this.Id + "'";
                SQL += ",'" + this.Nome + "'";
                SQL += ",'" + this.NomePai + "'";
                SQL += ",'" + this.NomeMae + "'";
                SQL += "," + Convert.ToString(this.NaoTemPai) + "";
                SQL += ",'" + this.Cpf + "'";
                SQL += "," + Convert.ToString(this.Genero) + "";
                SQL += ",'" + this.Cep + "'";
                SQL += ",'" + this.Logradouro + "'";
                SQL += ",'" + this.Complemento + "'";
                SQL += ",'" + this.Bairro + "'";
                SQL += ",'" + this.Cidade + "'";
                SQL += ",'" + this.Estado + "'";
                SQL += ",'" + this.Telefone + "'";
                SQL += ",'" + this.Profissao + "'";
                SQL += "," + Convert.ToString(this.RendaFamiliar) + ");";

                return SQL;
            }

            public string ToUpdate(string Id)
            {
                string SQL;

                SQL = @"UPDATE TB_Cliente
                        SET";
                SQL += "Id = '" + this.Id + "'";

                SQL += ", Nome = '" + this.Nome + "'";
                SQL += ", NomePai = '" + this.NomePai + "'";
                SQL += ", NomeMae = '" + this.NomeMae + "'";
                SQL += ", NaoTemPai = " + Convert.ToString(this.NaoTemPai) + "";
                SQL += ", Cpf = '" + this.Cpf + "'";
                SQL += ", Genero = " + Convert.ToString(this.Genero) + "";
                SQL += ", Cep = '" + this.Cep + "'";
                SQL += ", Logradouro = '" + this.Logradouro + "'";
                SQL += ", Complemento = '" + this.Complemento + "'";
                SQL += ", Bairro = '" + this.Bairro + "'";
                SQL += ", Cidade = '" + this.Cidade + "'";
                SQL += ", Estado = '" + this.Estado + "'";
                SQL += ", Telefone = '" + this.Telefone + "'";
                SQL += ", Profissao = '" + this.Profissao + "'";
                SQL += ", RendaFamiliar = " + Convert.ToString(this.RendaFamiliar) + "";
                SQL += "WHERE Id = '"+ Id +"';";

                return SQL;
            }


            #endregion

            #endregion
        }
        public class List
        {
            public List<Unit> ListUnit { get; set; }
        }

        public static Unit DesSerializedClassUnit(string vJson)
        {
            return JsonConvert.DeserializeObject<Unit>(vJson);
        }

        public static string SerializedClassUnit(Unit unit)
        {
            return JsonConvert.SerializeObject(unit);
        }

    }
}
