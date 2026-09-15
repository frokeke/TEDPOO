using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // Criação da academia.
        // A Academia possui listas próprias de alunos, planos, personal trainers e matrículas, caracterizando composição.
        Academia academia = new Academia();

        // Criação de dois alunos.
        // Cada aluno possui estado obrigatório (nome, CPF e peso), por isso a classe Aluno possui um construtor.
        Aluno aluno1 = new Aluno("Ryan", "123.169.426-48", 70.5m);
        Aluno aluno2 = new Aluno("Lukas", "123.168.423-48", 60.5m);

        // Criação dos planos.
        // Plano também possui estado obrigatório e, portanto, possui um construtor para inicializar seus dados.
        Plano planoMensal = new Plano("Mensal", 100.00m);
        Plano planoAnual = new Plano("Anual", 900.00m);

        // Associação entre a Academia e os alunos.
        // O objeto Aluno existe independentemente da Academia
        academia.CadastrarAluno(aluno1);
        academia.CadastrarAluno(aluno2);

        // Associação entre a Academia e os planos.
        academia.CadastrarPlano(planoMensal);
        academia.CadastrarPlano(planoAnual);

        // Matrícula do primeiro aluno no plano mensal.
        // Destinatário: academia
        // Seletor: MatricularAluno
        // Argumentos: aluno1, planoMensal e a data atual.
        Matricula matricula1 =
            academia.MatricularAluno(aluno1, planoMensal, DateTime.Now);

        // Matrícula do segundo aluno no plano anual.
        Matricula matricula2 =
            academia.MatricularAluno(aluno2, planoAnual, DateTime.Now);

        // Criação de um PersonalTrainer.
        PersonalTrainer personalTrainer =
            new PersonalTrainer("Rogerio", "123.169.429-48");

        // Associação entre Academia e PersonalTrainer.
        academia.CadastrarPersonalTrainer(personalTrainer);

        Console.WriteLine("////ALUNOS CADASTRADOS////");

        // Solicita à academia uma cópia da lista de alunos cadastrados.
        // A lista interna não é exposta diretamente, reforçando o encapsulamento.
        List<Aluno> alunos = academia.ListarAlunos();

        foreach (Aluno aluno in alunos)
        {
            Console.WriteLine(
                "Nome: " + aluno.Nome +
                " - CPF: " + aluno.Cpf +
                " - Peso: " + aluno.Peso + " kg");
        }

        Console.WriteLine();
        Console.WriteLine("////MATRÍCULAS////");

        // Solicita à academia uma cópia da lista de matrículas.
        List<Matricula> matriculas = academia.ListarMatriculas();

        foreach (Matricula matricula in matriculas)
        {
            Console.WriteLine(
                "Aluno: " + matricula.Aluno.Nome +
                " - Plano: " + matricula.Plano.Nome);
        }
    }
}


public class Aluno
{
    // Os atributos são private, garantindo encapsulamento.
    // O acesso externo ocorre através das propriedades e métodos públicos.
    private string nome;
    private string cpf;
    private decimal peso;

    // A lista de matrículas pertence ao aluno.
    // A classe Aluno controla a inclusão e consulta de suas matrículas.
    private List<Matricula> matriculas;

    // Construtor responsável por inicializar todo o estado obrigatório do aluno.
    public Aluno(string nome, string cpf, decimal peso)
    {
        // Regra de validação para impedir um peso inválido.
        if (peso <= 0)
        {
            throw new ArgumentException("O peso deve ser maior que zero.");
        }

        this.nome = nome;
        this.cpf = cpf;
        this.peso = peso;

        // Inicialização da coleção interna.
        matriculas = new List<Matricula>();
    }

    // Propriedade somente leitura.
    // O código externo pode consultar o nome, mas não alterá-lo diretamente.
    public string Nome
    {
        get { return nome; }
    }

    // Propriedade somente leitura para preservar o encapsulamento do CPF.
    public string Cpf
    {
        get { return cpf; }
    }

    // Propriedade somente leitura.
    // A alteração do peso deve ser feita pelo método AtualizarPeso().
    public decimal Peso
    {
        get { return peso; }
    }

    // Método responsável por alterar o peso do aluno.
    // A regra de negócio fica encapsulada dentro da própria classe.
    public void AtualizarPeso(decimal novoPeso)
    {
        if (novoPeso <= 0)
        {
            throw new ArgumentException("O peso deve ser maior que zero.");
        }

        peso = novoPeso;
    }

    // Adiciona uma matrícula à coleção do aluno.
    public void AdicionarMatricula(Matricula matricula)
    {
        if (matricula == null)
        {
            throw new ArgumentNullException(nameof(matricula));
        }

        matriculas.Add(matricula);
    }

    // Retorna uma cópia da lista para impedir que código externo altere diretamente a coleção interna do aluno.
    public List<Matricula> ListarMatriculas()
    {
        return new List<Matricula>(matriculas);
    }
}


public class Plano
{
    // Atributos privados para garantir encapsulamento.
    private string nome;
    private decimal valorMensal;

    // Construtor que inicializa o estado obrigatório do plano.
    public Plano(string nome, decimal valorMensal)
    {
        if (valorMensal <= 0)
        {
            throw new ArgumentException(
                "O valor mensal deve ser maior que zero.");
        }

        this.nome = nome;
        this.valorMensal = valorMensal;
    }

    // Permite consultar o nome sem permitir alteração direta.
    public string Nome
    {
        get { return nome; }
    }

    // Permite consultar o valor sem permitir alteração direta.
    public decimal ValorMensal
    {
        get { return valorMensal; }
    }
}


public class Matricula
{
    // Os atributos são privados, mantendo o encapsulamento.
    private Aluno aluno;
    private Plano plano;
    private DateTime dataInicio;
 
    //A matrícula depende de um aluno, um plano e uma data.
    
    public Matricula(
        Aluno aluno,
        Plano plano,
        DateTime dataInicio)
    {
        // Validação para garantir que uma matrícula tenha um aluno.
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        // Validação para garantir que uma matrícula tenha um plano.
        if (plano == null)
        {
            throw new ArgumentNullException(nameof(plano));
        }

        this.aluno = aluno;
        this.plano = plano;
        this.dataInicio = dataInicio;
    }

    // Permite consultar o aluno associado à matrícula.
    public Aluno Aluno
    {
        get { return aluno; }
    }

    // Permite consultar o plano associado à matrícula.
    public Plano Plano
    {
        get { return plano; }
    }

    // Permite consultar a data de início da matrícula.
    public DateTime DataInicio
    {
        get { return dataInicio; }
    }
}


public class PersonalTrainer
{
    // Atributos privados: encapsulamento aplicado.
    private string nome;
    private string cpf;

    // Construtor que inicializa o estado obrigatório.
    public PersonalTrainer(string nome, string cpf)
    {
        this.nome = nome;
        this.cpf = cpf;
    }

    // Propriedade somente leitura.
    public string Nome
    {
        get { return nome; }
    }

    // Propriedade somente leitura.
    public string Cpf
    {
        get { return cpf; }
    }
}


public class Academia
{
    /* 
    Todas as coleções são privadas.
    Dessa forma, outras classes não conseguem modificar diretamente o estado interno da Academia.
    */
    private List<Aluno> alunos;
    private List<Plano> planos;
    private List<PersonalTrainer> personalTrainers;
    private List<Matricula> matriculas;

    /* 
    Construtor da Academia.
    As listas são inicializadas para que a academia possa receber alunos, planos, personal trainers e matrículas
    */
    public Academia()
    {
        alunos = new List<Aluno>();
        planos = new List<Plano>();
        personalTrainers = new List<PersonalTrainer>();
        matriculas = new List<Matricula>();
    }

    // Cadastra um aluno na academia.
    public void CadastrarAluno(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        alunos.Add(aluno);
    }

    // Cadastra um plano na academia.
    public void CadastrarPlano(Plano plano)
    {
        if (plano == null)
        {
            throw new ArgumentNullException(nameof(plano));
        }

        planos.Add(plano);
    }

    // Cadastra um personal trainer na academia.
    public void CadastrarPersonalTrainer(
        PersonalTrainer personalTrainer)
    {
        if (personalTrainer == null)
        {
            throw new ArgumentNullException(nameof(personalTrainer));
        }

        personalTrainers.Add(personalTrainer);
    }

    // Realiza a matrícula de um aluno em um plano.
    public Matricula MatricularAluno(
        Aluno aluno,
        Plano plano,
        DateTime dataInicio)
    {
        // O aluno precisa estar previamente cadastrado.
        if (!alunos.Contains(aluno))
        {
            throw new Exception(
                "O aluno não está cadastrado na academia.");
        }

        // O plano precisa estar previamente cadastrado.
        if (!planos.Contains(plano))
        {
            throw new Exception(
                "O plano não está cadastrado na academia.");
        }

        /* 
        Criação da matrícula;
        A Academia cria e mantém a matrícula, estabelecendo uma relação entre Aluno e Plano.
        */
        Matricula matricula =
            new Matricula(aluno, plano, dataInicio);

        // A matrícula passa a fazer parte da coleção da Academia.
        matriculas.Add(matricula);

        /*
        Associação da matrícula ao aluno;

        Destinatário: aluno
        Seletor: AdicionarMatricula
        Argumento: matricula
        */
        aluno.AdicionarMatricula(matricula);

        return matricula;
    }

    /* 
    Retorna uma cópia da lista de alunos;
    Isso impede que a lista interna seja modificada diretamente
    */
    public List<Aluno> ListarAlunos()
    {
        return new List<Aluno>(alunos);
    }

    // Retorna uma cópia da lista de planos
    public List<Plano> ListarPlanos()
    {
        return new List<Plano>(planos);
    }

    // Retorna uma cópia da lista de personal trainers
    public List<PersonalTrainer> ListarPersonalTrainers()
    {
        return new List<PersonalTrainer>(personalTrainers);
    }

    // Retorna uma cópia da lista de matrículas
    public List<Matricula> ListarMatriculas()
    {
        return new List<Matricula>(matriculas);
    }
}