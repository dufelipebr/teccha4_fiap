using apibronco.bronco.com.br.DTOs;
using apibronco.bronco.com.br.Entity;
using Bogus;
using Bogus.DataSets;
using Moq;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.Xml;
using Xunit;



namespace apibronco.xunit
{
    /*
     * 
     * AssertionConcern.AssertArgumentNotEmpty(Codigo, "Codigo precisa ser preenchido.");
        AssertionConcern.AssertArgumentLength(Codigo, 50, "Codigo do Investimento precisa ter no maximo 10 caracteres.");
        AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
        AssertionConcern.AssertArgumentLength(Nome, 100, "Nome do Investimento precisa ter no maximo 50 caracteres.");
        AssertionConcern.AssertArgumentNotEquals(TipoInvestimento, 0, "Tipo Investimento precisa ser preenchido");
        AssertionConcern.AssertArgumentRange((double)TaxaADM, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
        AssertionConcern.AssertArgumentRange((double)AporteMinimo, 0.1, 1000000, "Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00");
     * 
     */
    public class TestCorretorOnline
    {
        private readonly Faker _faker;
        //private readonly portalinvestimento.virtualtilab.com.StringDictionary _sd;

        public TestCorretorOnline()
        {
            _faker = new Faker();
            //_sd = new portalinvestimento.virtualtilab.com.StringDictionary();
        }

        /// <summary>
        /// Validar a criação de registro de usuario parecido com o registrar_usuario
        /// </summary>
        [Fact]
        [Trait("Categoria", "CorretorOnline")]
        public void RegistrarUsuario_ShoulReturnSuccessMessage()
        {
            //var mock = new Mock<IInvestimentoService>();
            //mock.Setup(service => service.Create(It.IsAny<Investimento>())).Returns("Investimento ok!"); // codigo retorno succeso
            //InvestimentoService s = new InvestimentoService();

            //Investimento investimento = new Investimento(
            //        Investimento.enTipoInvestimento.CDI,
            //        "Simples Automático RF",
            //        "Fundo mais simples do bradesco destinado para o povão",
            //        "Simples_Automático_RF",
            //        1.5m,
            //        1.0m,
            //        2.5m,
            //        11.25m,
            //        24.06m);
            ////IInvestimentoService investimentoService = mockInvestimentoService.Object;
            ////act 
            //var resultaEsperado = mock.Object.Create(investimento);
            //string resultado = s.Create(investimento);
                
            //Assert.Equal(resultaEsperado, resultado);
        }

        [Fact]
        [Trait("Categoria", "Validando RegisterInfo")]
        public void Cliente_Segurado_ShouldBeValid()
        {
            RegisterInfo info= new RegisterInfo();
            //info.cpf = _faker.Random.AlphaNumeric(15);
            info.cpf = "291.995.888-70";
            info.option_renda = 1;
            info.rg = _faker.Random.AlphaNumeric(15);
            info.profissao = "analista";
            info.data_nascimento = new DateTime(1981, 6, 7);
            info.email = "bronco@bronco.com.br";
            info.flag_possui_nome_social = true;
            info.nome_social = _faker.Random.String2(50);
            info.genero = 'F';
            info.profissao = _faker.Random.String2(50);
            info.nome = _faker.Random.String2(50);
            info.sobre_nome = _faker.Random.String2(50);
            info.telefone = string.Join("", _faker.Random.Digits(8, 1, 9));
            //act
            //var result = Assert.Throws<ArgumentException>(() => 
            //    new Cliente_Segurado(info)
            //);
            ////Assert.Throws<DomainException>()

            ////Assert
            //Assert.Equal("", result.Message);

            string result = "";
            try
            {
                Cliente_Segurado u = new Cliente_Segurado(info);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            Assert.Equal("", result);

        }


        [Fact]
        [Trait("Categoria", "CPF Invalido")]
        public void Cliente_Segurado_ShouldBeInValid_CPF()
        {
            RegisterInfo info = new RegisterInfo();
            info.cpf = _faker.Random.AlphaNumeric(15);
            //info.cpf = "291.995.888-70";
            info.option_renda = 1;
            info.rg = _faker.Random.AlphaNumeric(15);
            info.profissao = "analista";
            info.data_nascimento = new DateTime(1981, 6, 7);
            info.email = "bronco@bronco.com.br";
            info.flag_possui_nome_social = true;
            info.nome_social = _faker.Random.String2(50);
            info.genero = 'F';
            info.profissao = _faker.Random.String2(50);
            info.nome = _faker.Random.String2(50);
            info.sobre_nome = _faker.Random.String2(50);
            info.telefone = string.Join("", _faker.Random.Digits(8, 1, 9));
            //act
            //var result = Assert.Throws<ArgumentException>(() => 
            //    new Cliente_Segurado(info)
            //);
            ////Assert.Throws<DomainException>()

            ////Assert
            //Assert.Equal("", result.Message);

            string result = "";
            try
            {
                Cliente_Segurado u = new Cliente_Segurado(info);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            Assert.Equal("CPF invalido!", result);

        }


        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenNome_Empty()
        //{
        //    // Arrange
        //    var tipo = Investimento.enTipoInvestimento.CDI;
        //    var nome = string.Empty;
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = 1.0m;
        //    var aporteMinimo = 10m;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() => new Investimento(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Nome precisa ser preenchido.", result.Message);

        //}


        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenNome_Higher()
        //{
        //    // Arrange
        //    var tipo = Investimento.enTipoInvestimento.CDI;
        //    var nome = _faker.Random.String2(101);
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = 1.0m;
        //    var aporteMinimo = 10m;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() => new Investimento(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Nome do Investimento precisa ter no maximo 100 caracteres.", result.Message);

        //}

        //// Não precisa de teste porque não é possivel setar um valor diferente Empty para o inteiro.
        ////[Fact]
        ////[Trait("Categoria", "Validando Investimento")]
        ////public void Investimento_ShouldThrowException_WhenTipoInvestimento_Empty()
        ////{
        ////    // Arrange
        ////    int tipo = 99; //Investimento.enTipoInvestimento.CDI;
        ////    var nome = _faker.Random.String2(100);
        ////    var descricao = _faker.Random.String2(500);
        ////    var codigo = _faker.Random.String2(10);
        ////    var taxaADM = 1.0m;
        ////    var aporteMinimo = 10m;
        ////    var rent_3 = 7.1m;
        ////    var rent_12 = 20.3m;
        ////    var rent_24 = 40.0m;

        ////    //act
        ////    var result = Assert.Throws<DomainException>(() => new Investimento((enTipoInvestimento)tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        ////    //Assert.Throws<DomainException>()

        ////    //Assert
        ////    Assert.Equal("Tipo Investimento precisa ser preenchido", result.Message);

        ////}


        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenTaxaAdm_Lower()
        //{
        //    // Arrange
        //    var tipo = Investimento.enTipoInvestimento.CDI;
        //    var nome = _faker.Random.String2(100);
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = -0.1m;
        //    var aporteMinimo = 10m;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() => new Investimento(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Taxa ADM precisa estar entre 0.1 e 10.", result.Message);

        //}

        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenTaxaAdm_Higher()
        //{
        //    // Arrange
        //    var tipo = Investimento.enTipoInvestimento.CDI;
        //    var nome = _faker.Random.String2(100);
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = 10.5m;
        //    var aporteMinimo = 10m;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() => new Investimento(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Taxa ADM precisa estar entre 0.1 e 10.", result.Message);

        //}

        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenAporteMinimo_Lower()
        //{
        //    // Arrange
        //    var tipo = Investimento.enTipoInvestimento.CDI;
        //    var nome = _faker.Random.String2(100);
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = 10m;
        //    var aporteMinimo = 0;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() => 
        //    new Investimento(
        //        tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00", result.Message);

        //}


        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenAporteMinimo_Higher()
        //{
        //    // Arrange
        //    var tipo = Investimento.enTipoInvestimento.CDI;
        //    var nome = _faker.Random.String2(100);
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = 10m;
        //    var aporteMinimo = 20000000000;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() =>
        //    new Investimento(
        //        tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00", result.Message);

        //}











    }
}