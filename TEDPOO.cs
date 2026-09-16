using System;
using System.Collections.Generic;

public class Aluno
{
    /*
    Os atributos são private, garantindo encapsulamento.
    O acesso externo ocorre através das propriedades e métodos públicos
    */
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public decimal Peso { get; private set; }

    private List<Matricula> matriculas;

    // Construtor responsável por inicializar aluno
    public Aluno(string nome, string cpf, decimal peso)
    {
        if (peso <= 0)
        {
            throw new ArgumentException("O peso deve ser maior que zero.");
        }

        Nome = nome;
        Cpf = cpf;
        Peso = peso;

        matriculas = new List<Matricula>();
    }

    public void AtualizarPeso(decimal novoPeso)
    {
        if (novoPeso <= 0)
        {
            throw new ArgumentException("O peso deve ser maior que zero.");
        }

        Peso = novoPeso;
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

    // Retorna uma cópia da lista para impedir que código externo altere diretamente a coleção interna do aluno
    public List<Matricula> ListarMatriculas()
    {
        return new List<Matricula>(matriculas);
    }
}


public class Plano
{
    // Atributos privados para garantir encapsulamento.
    public string Nome { get; private set; }
    public decimal ValorMensal { get; private set; }

    // Construtor que inicializa o plano
    public Plano(string nome, decimal valorMensal)
    {
        if (valorMensal <= 0)
        {
            throw new ArgumentException(
                "O valor mensal deve ser maior que zero.");
        }

        Nome = nome;
        ValorMensal = valorMensal;
    }
}


public class Matricula
{
    // A alteração dos atributos é privada à construtores, mantendo o encapsulamento
    public Aluno Aluno { get; private set; }
    public Plano Plano { get; private set; }
    public DateTime DataInicio { get; private set; }

    //A matrícula depende de um aluno, um plano e uma data
    public Matricula(Aluno aluno, Plano plano, DateTime dataInicio)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        if (plano == null)
        {
            throw new ArgumentNullException(nameof(plano));
        }

        Aluno = aluno;
        Plano = plano;
        DataInicio = dataInicio;
    }
}


public class PersonalTrainer
{
    // Atributos privados
    public string Nome { get; private set; }
    public string Cpf { get; private set; }

    public PersonalTrainer(string nome, string cpf)
    {
        Nome = nome;
        Cpf = cpf;
    }
}


public class Academia
{
    /* 
    Todas as coleções são privadas.
    Dessa forma, outras classes não conseguem modificar diretamente o estado interno da Academia
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
    public Matricula MatricularAluno(Aluno aluno, Plano plano, DateTime dataInicio)
    {
        if (!alunos.Contains(aluno))
        {
            throw new Exception(
                "O aluno não está cadastrado na academia.");
        }

        if (!planos.Contains(plano))
        {
            throw new Exception(
                "O plano não está cadastrado na academia.");
        }

        /* 
        Criação da matrícula;
        A Academia cria e mantém a matrícula, estabelecendo uma relação entre Aluno e Plano.
        */
        Matricula matricula = new Matricula(aluno, plano, dataInicio);

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

public class Program
{
    public static void Main()
    {
        /*
        Criação da academia.
        Relação de composiçãp entre Academia e Matrícula
        */
        Academia academia = new Academia();

        /*
        Criação de dois alunos.
        Cada aluno possui estado obrigatório (nome, CPF e peso)
        */
        Aluno aluno1 = new Aluno("Ryan", "123.169.426-48", 40.5m);
        Aluno aluno2 = new Aluno("Lukas", "123.168.423-48", 50.5m);

        /*
        Criação dos planos.
        Plano também possui estado obrigatório(nome e valor)
        */
        Plano planoMensal = new Plano("Mensal", 100.00m);
        Plano planoAnual = new Plano("Anual", 900.00m);

        /*
        Associação entre a Academia e os alunos.
        O objeto Aluno existe independentemente da Academia
        */
        academia.CadastrarAluno(aluno1);
        academia.CadastrarAluno(aluno2);

        // Associação entre a Academia e os planos
        academia.CadastrarPlano(planoMensal);
        academia.CadastrarPlano(planoAnual);

        /*
        Matrícula do primeiro aluno no plano mensal.
        Destinatário: academia
        Seletor: MatricularAluno
        Argumentos: aluno1, planoMensal e a data atual.
        */
        Matricula matricula1 = academia.MatricularAluno(aluno1, planoMensal, DateTime.Now);

        // Matrícula do segundo aluno no plano anual.
        Matricula matricula2 = academia.MatricularAluno(aluno2, planoAnual, DateTime.Now);

        // Criação de um PersonalTrainer.
        PersonalTrainer personalTrainer = new PersonalTrainer("Rogerio", "123.169.429-48");

        // Associação entre Academia e PersonalTrainer.
        academia.CadastrarPersonalTrainer(personalTrainer);

        Console.WriteLine("---ALUNOS CADASTRADOS---");
        List<Aluno> alunos = academia.ListarAlunos();

        foreach (Aluno aluno in alunos)
        {
            Console.WriteLine("Nome: " + aluno.Nome + " - CPF: " + aluno.Cpf + " - Peso: " + aluno.Peso + " kg");
        }

        Console.WriteLine();
        Console.WriteLine("---MATRÍCULAS---");

        // Solicita à academia uma cópia da lista de matrículas.
        List<Matricula> matriculas = academia.ListarMatriculas();

        foreach (Matricula matricula in matriculas)
        {
            Console.WriteLine( "Aluno: " + matricula.Aluno.Nome + " - Plano: " + matricula.Plano.Nome);
        }
    }
}


