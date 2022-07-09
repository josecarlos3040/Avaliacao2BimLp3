using Avaliacao2BimLp3.Database;
using Avaliacao2BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var studentRepository = new StudentRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Student List");
        if (studentRepository.GetAll().Count == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado");
        }
        else
        {
            foreach (var student in studentRepository.GetAll())
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, não formado");
                }       
            }
        }
    }

    if(modelAction == "ListByCities")
    {
        string[] vetor = new string[args.Length - 2];
        for(int i = 2; i < args.Length; i++)
        {
            vetor[i-2] = args[i];
        } 
        Console.WriteLine("Student ListByCities");
        if (studentRepository.GetAllByCities(vetor).Count == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado nessas cidades");
        }
        else
        {
            foreach (var student in studentRepository.GetAllByCities(vetor))
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, não formado");
                }      
            }
        }
    }

    if(modelAction == "ListByCity")
    {
        if (studentRepository.GetAllStudentByCity(args[2]).Count == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado nessa cidade");
        }
        else
        {
            foreach (var student in studentRepository.GetAllStudentByCity(args[2]))
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, não formado");
                }      
            }
        }
    }

    if(modelAction == "ListFormed")
    {
        Console.WriteLine("Student ListFormed");
        if (studentRepository.GetAllFormed().Count == 0)
        {
            Console.WriteLine("Nenhum estudante formado cadastrado");
        }
        else
        {
            foreach (var student in studentRepository.GetAllFormed())
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");       
            }
        }
    }

    if(modelAction == "New")
    {
        if(studentRepository.ExistsById(args[2]))
        {
            Console.WriteLine($"Student de registro {args[2]} já existe");
        }
        else
        {
            var product = new Student(args[2], args[3], args[4], Convert.ToBoolean(args[5]));
            studentRepository.Save(product);
            Console.WriteLine($"Student {args[3]} adicionado");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        if(studentRepository.ExistsById(args[2]))
        {
            studentRepository.MarkAsFormed(args[2]);
            Console.WriteLine($"Student {args[2]} definido como formado");
        }
        else
        {
            Console.WriteLine($"O Student com registro {args[2]} não foi encontrado");
        }
    }

    if(modelAction == "Delete")
    {
        if(studentRepository.ExistsById(args[2]))
        {
            studentRepository.Delete(args[2]);
            Console.WriteLine($"O Student de registro {args[2]} foi removido");
        }
        else
        {
            Console.WriteLine($"O Student de registro {args[2]} não foi encontrado");
        }
    }

    if(modelAction == "Report")
    {
        if(args[2] == "CountByCities")
        {
            Console.WriteLine("Student CountByCities");
            if(studentRepository.CountByCities().Count == 0)
            {
                Console.WriteLine($"Nenhum estudante cadastrado");
            }
            else
            {
                foreach (var student in studentRepository.CountByCities())
                {
                    Console.WriteLine($"{student.AttributeName}, {student.StudentNumber}");       
                }
            }
        }

        if(args[2] == "CountByFormed")
        {
            if(studentRepository.CountByFormed().Count == 0)
            {
                Console.WriteLine($"Nenhum estudante cadastrado");
            }
            else
            {
                foreach (var student in studentRepository.CountByFormed())
                {
                    if(student.AttributeName == "1")
                    {
                        Console.WriteLine($"Formado, {student.StudentNumber}");
                    }   
                    else
                    {
                        Console.WriteLine($"Não Formado, {student.StudentNumber}");
                    }    
                }
            }
        }
    }
}