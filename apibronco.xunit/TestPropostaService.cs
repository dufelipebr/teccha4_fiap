using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.DTOs;


namespace apibronco.xunit
{
    public class TestPropostaService
    {

        [Fact]
        [Trait("Usuario", "Proposta")]
        public void Proposta_ShoulReturnSuccessMessage()
        {
            //// Arrange
            //CadastrarUsuarioDTO dto = new CadastrarUsuarioDTO();
            //dto.Email = "du.felipe.br@gmail.com";
            //dto.Senha = "123456";
            //dto.Saldo_Carteira = 0;
            //dto.Codigo_Conta = 1;
            //dto.Digito_Conta = 1;
            //dto.CPF = "291.995.888-70";
            //dto.Nome = "carlos oliveira";
            //dto.Senha = "@123456cC";
            ////act

            //var result = "";
            //try
            //{
            //    Usuario u = new Usuario(dto);
            //}
            //catch(Exception ex)
            //{
            //    result = ex.Message;
            //}
            
            //Assert.Equal("", result);
            ////Assert.Throws<DomainException>()

            ////Assert


        }

        //[Fact]
        //[Trait("Usuario", "Validando Usuario")]
        //public void Usuario_Validate_AlterarUsuarioDTO()
        //{
        //    // Arrange
        //    AlterarUsuarioDTO dto = new AlterarUsuarioDTO();
        //    //dto.Email = "du.felipe.br@gmail.com";
        //    //dto.Senha = "123456";
        //    dto.Saldo_Carteira = 10m;
        //    //dto.Codigo_Conta = 1;
        //    //dto.Digito_Conta = 1;
        //    //dto.CPF = "291.995.888-70";
        //    //dto.Nome = "carlos oliveira";
        //    //dto.Senha = "@123456cC";
        //    dto.Id = 1;
        //    //act

        //    var result = "";
        //    try
        //    {
        //        Usuario u = new Usuario(dto);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }

        //    Assert.Equal("", result);
        //    //Assert.Throws<DomainException>()

        //    //Assert


        //}

        ////[Fact]
        ////[Trait("Usuario", "Validando Email")]
        ////public void Usuario_Valid_Email()
        ////{
        ////    // Arrange

        ////    var email1 = "du.felipe.br@gmail.com";
        ////    var email2 = "dufelipebr@gmail.com";
        ////    var email3 = "dugmail.com";

        ////    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        ////    //var emailRegex = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i;

        ////    //Assert
        ////    Assert.Equal(true, regex.IsMatch(email1));
        ////    //Assert.Equal(true, regex.IsMatch(email2));
        ////    //Assert.Equal(true, regex.IsMatch(email3));

        ////}

        ////[Fact]
        ////[Trait("Usuario", "Email invalido")]
        ////public void Usuario_Invalid_Email()
        ////{
        ////    // Arrange

        ////    var email1 = "du343####$%¨&*gmail.com";

        ////    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        ////    //var emailRegex = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i;

        ////    //Assert
        ////    Assert.Equal(false, regex.IsMatch(email1));
        ////    //Assert.Equal(true, regex.IsMatch(email2));
        ////    //Assert.Equal(true, regex.IsMatch(email3));

        ////}

        //[Fact]
        //[Trait("Usuario", "Email invalido")]
        //public void Usuario_Testing_Valid_CPF()
        //{
        //    // Arrange

        //    Regex regex = new Regex(@"^\d{3}.?\d{3}.?\d{3}-?\d{2}$");
        //    //Regex regex = new Regex(@"/^([0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}|[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2})$/");

        //    var option1 = "291.995.888-70";
        //    var option2 = "256.969.588-26";
        //    var option3= "291.995.82/0001-80";
        //    var option4 = "29b.995.aa4-70";
        //    var option5 = "291.295.888/70";
        //    var option6 = "291995888-80";
        //    var option7 = "29199588880";


        //    var expectations = new List<Tuple<object, object>>()
        //    {
        //        new(true, regex.IsMatch(option1)),
        //        new(true, regex.IsMatch(option2)),
        //        new(false, regex.IsMatch(option3)),
        //        new(false, regex.IsMatch(option4)),
        //        new(false, regex.IsMatch(option5)),
        //        new(true, regex.IsMatch(option6)),
        //        new(true, regex.IsMatch(option7)),
        //    };
        //    Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));


          


        //    //var emailRegex = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i;

        //    //Assert
        //    ////Assert.Equal(false, regex.IsMatch(email1));
        //    ////Assert.Equal(true, regex.IsMatch(email2));
        //    //Assert.Equal(true, regex.IsMatch(email3));

        //}


        //[Fact]
        //public void Usuario_Testing_Valid_Email()
        //{
        //    // Arrange

        //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        //    var email1 = "du343####$%¨&*gmail.com";
        //    var email2 = "du329420.hotmail.com";
        //    var email3 = "du343####$%@¨&*gmail.com";
        //    var email4 = "1234567asdjhaksjd@";
        //    var email5 = "carlos_oliveira@swissre.com";
        //    var email6 = "du.felipe.br@gmail.com";
        //    var email7 = "cadu@box.com";

        //    var expectations = new List<Tuple<object, object>>()
        //    {
        //        new(false, regex.IsMatch(email1)),
        //        new(false, regex.IsMatch(email2)),
        //        new(false, regex.IsMatch(email3)),
        //        new(false, regex.IsMatch(email4)),
        //        new(true, regex.IsMatch(email5)),
        //        new(true, regex.IsMatch(email6)),
        //        new(true, regex.IsMatch(email7)),
        //    };
        //    Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));





        //    //var emailRegex = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i;

        //    //Assert
        //    ////Assert.Equal(false, regex.IsMatch(email1));
        //    ////Assert.Equal(true, regex.IsMatch(email2));
        //    //Assert.Equal(true, regex.IsMatch(email3));

        //}

        //[Fact]
        //public void Usuario_Testing_Valid_Password()
        //{
        //    // Arrange
        //    //1.Min 8 char and max 14 char
        //    //2.One upper case
        //    //3.Atleast one lower case
        //    //4.No white space
        //    //5.Check for one special character

        //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        //    var option1 = "123456"; // fail 1 & multiple
        //    var option2 = "@carlos123456"; // fail 2
        //    var option3 = "Abcjdkjdjksdjks903290ls";// fail 1 - max 14
        //    var option4 = "asdjhkjasd"; // fail 1 - max 14
        //    var option5 = "@123 456C";// fail 4
        //    var option6 = "@123456cC";// OK 
            


        //    UsuarioService us = new UsuarioService();
        //    var expectations = new List<Tuple<object, object>>()
        //    {
        //        new(false, us.CheckPassword(option1)),
        //        new(false, us.CheckPassword(option2)),
        //        new(false, us.CheckPassword(option3)),
        //        new(false, us.CheckPassword(option4)),
        //        new(false, us.CheckPassword(option5)),
        //        new(true, us.CheckPassword(option6))

        //    };
        //    Assert.All(expectations, pair => Assert.Equal(pair.Item1, pair.Item2));





        //    //var emailRegex = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i;

        //    //Assert
        //    ////Assert.Equal(false, regex.IsMatch(email1));
        //    ////Assert.Equal(true, regex.IsMatch(email2));
        //    //Assert.Equal(true, regex.IsMatch(email3));

        //}
    }
}
