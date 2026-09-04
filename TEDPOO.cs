using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Academia academia = new Academia();

        Aluno aluno1 = new Aluno("Ryan", "123.169.426-48", 70.5m);
        Aluno aluno2 = new Aluno("Lukas", "123.168.423-48", 60.5m);

        Plano planoMensal = new Plano("Mensal", 100.00m);
        Plano planoAnual = new Plano("Anual", 900.00m);

        academia.CadastrarAluno(aluno1);
        academia.CadastrarAluno(aluno2);

        academia.CadastrarPlano(planoMensal);
        academia.CadastrarPlano(planoAnual);

        Matricula matricula1 = academia.MatricularAluno(aluno1, planoMensal, DateTime.Now);

        Matricula matricula2 = academia.MatricularAluno(aluno2, planoAnual, DateTime.Now);

        PersonalTrainer personalTrainer = new PersonalTrainer("Rogerio", "123.169.429-48");

        academia.CadastrarPersonalTrainer(personalTrainer);

        Console.WriteLine("////ALUNOS CADASTRADOS////");

        List<Aluno> alunos = academia.ListarAlunos();

        foreach (Aluno aluno in alunos)
        {
            Console.WriteLine(
                "Nome: " + aluno.Nome + " - CPF: " + aluno.Cpf + " - Peso: " + aluno.Peso + " kg");
        }

        Console.WriteLine();
        Console.WriteLine("////MATRÍCULAS////");

        List<Matricula> matriculas = academia.ListarMatriculas();

        foreach (Matricula matricula in matriculas)
        {
            Console.WriteLine(
                "Aluno: " + matricula.Aluno.Nome + " - Plano: " + matricula.Plano.Nome);
        }
    }
}


public class Aluno
{
    private string nome;
    private string cpf;
    private decimal peso;

    private List<Matricula> matriculas;

    public Aluno(string nome, string cpf, decimal peso)
    {
        if (peso <= 0)
        {
            throw new ArgumentException("O peso deve ser maior que zero.");
        }

        this.nome = nome;
        this.cpf = cpf;
        this.peso = peso;

        matriculas = new List<Matricula>();
    }

    public string Nome
    {
        get { return nome; }
    }

    public string Cpf
    {
        get { return cpf; }
    }

    public decimal Peso
    {
        get { return peso; }
    }

    public void AtualizarPeso(decimal novoPeso)
    {
        if (novoPeso <= 0)
        {
            throw new ArgumentException("O peso deve ser maior que zero.");
        }

        peso = novoPeso;
    }

    public void AdicionarMatricula(Matricula matricula)
    {
        if (matricula == null)
        {
            throw new ArgumentNullException(nameof(matricula));
        }
        matriculas.Add(matricula);
    }

    public List<Matricula> ListarMatriculas()
    {
        return new List<Matricula>(matriculas);
    }
}

public class Plano
{
    private string nome;
    private decimal valorMensal;

    public Plano(string nome, decimal valorMensal)
    {
        if (valorMensal <= 0)
        {
            throw new ArgumentException("O valor mensal deve ser maior que zero.");
        }

        this.nome = nome;
        this.valorMensal = valorMensal;
    }

    public string Nome
    {
        get { return nome; }
    }

    public decimal ValorMensal
    {
        get { return valorMensal; }
    }
}

public class Matricula
{
    private Aluno aluno;
    private Plano plano;
    private DateTime dataInicio;

    public Matricula(
        Aluno aluno,
        Plano plano,
        DateTime dataInicio)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        if (plano == null)
        {
            throw new ArgumentNullException(nameof(plano));
        }

        this.aluno = aluno;
        this.plano = plano;
        this.dataInicio = dataInicio;
    }

    public Aluno Aluno
    {
        get { return aluno; }
    }

    public Plano Plano
    {
        get { return plano; }
    }

    public DateTime DataInicio
    {
        get { return dataInicio; }
    }
}

public class PersonalTrainer
{
    private string nome;
    private string cpf;

    public PersonalTrainer(string nome, string cpf)
    {
        this.nome = nome;
        this.cpf = cpf;
    }

    public string Nome
    {
        get { return nome; }
    }

    public string Cpf
    {
        get { return cpf; }
    }
}

public class Academia
{
    private List<Aluno> alunos;
    private List<Plano> planos;
    private List<PersonalTrainer> personalTrainers;
    private List<Matricula> matriculas;

    public Academia()
    {
        alunos = new List<Aluno>();
        planos = new List<Plano>();
        personalTrainers = new List<PersonalTrainer>();
        matriculas = new List<Matricula>();
    }

    public void CadastrarAluno(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        alunos.Add(aluno);
    }

    public void CadastrarPlano(Plano plano)
    {
        if (plano == null)
        {
            throw new ArgumentNullException(nameof(plano));
        }

        planos.Add(plano);
    }

    public void CadastrarPersonalTrainer(
        PersonalTrainer personalTrainer)
    {
        if (personalTrainer == null)
        {
            throw new ArgumentNullException(nameof(personalTrainer));
        }

        personalTrainers.Add(personalTrainer);
    }

    public Matricula MatricularAluno(Aluno aluno, Plano plano, DateTime dataInicio)
    {
        if (!alunos.Contains(aluno))
        {
            throw new Exception("O aluno não está cadastrado na academia.");
        }

        if (!planos.Contains(plano))
        {
            throw new Exception("O plano não está cadastrado na academia.");
        }

        Matricula matricula = new Matricula(aluno, plano, dataInicio);

        matriculas.Add(matricula);

        aluno.AdicionarMatricula(matricula);

        return matricula;
    }

    public List<Aluno> ListarAlunos()
    {
        return new List<Aluno>(alunos);
    }

    public List<Plano> ListarPlanos()
    {
        return new List<Plano>(planos);
    }

    public List<PersonalTrainer> ListarPersonalTrainers()
    {
        return new List<PersonalTrainer>(personalTrainers);
    }

    public List<Matricula> ListarMatriculas()
    {
        return new List<Matricula>(matriculas);
    }
}
