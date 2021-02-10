using SchedulingPatients.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingPatients.MVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ScheduleContext context)
        {
            context.Database.EnsureCreated();

            // Verifica se existe algum paciente
            if(context.Patients.Any())
            {
                return; // banco de dados foi semeado;
            }

            var patients = new Patient[]
            {
                new Patient{FirstName = "Ana Lúcia", LastName = "Arcieri Calasans da Silva", BirthDate = DateTime.Parse("2003-05-15"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Brenda", LastName = "De Jesus da Silva", BirthDate = DateTime.Parse("1995-10-09"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Katharina Lúcia", LastName = "De Almeida Moraes Fraga", BirthDate = DateTime.Parse("1993-09-12"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Maria Eliana", LastName = "Santos da Costa", BirthDate = DateTime.Parse("1963-09-30"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Camila", LastName = "Vitoria Santos", BirthDate = DateTime.Parse("2004-08-22"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Jocenia", LastName = "Alves Santos", BirthDate = DateTime.Parse("1969-02-03"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Cláudia", LastName = "De Oliveira Santos", BirthDate = DateTime.Parse("1982-07-18"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Daniela", LastName = "Maria Gomes Carvalho", BirthDate = DateTime.Parse("2000-05-01"), Genre = Genre.FEMININO},
                new Patient{FirstName = "Lidia Carla", LastName = "Souza de Oliveira", BirthDate = DateTime.Parse("1996-10-14"), Genre = Genre.FEMININO}
            };

            foreach(Patient p in patients)
            {
                context.Patients.Add(p);
            }
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee { FirstName = "Júlio", LastName = "César", HireDate = DateTime.Parse("2004-03-29") },
                new Employee { FirstName = "Emília", LastName = "Correia", HireDate = DateTime.Parse("2004-03-29") },
                new Employee { FirstName = "Márcio", LastName = "Liula", HireDate = DateTime.Parse("2004-03-29") },
                new Employee { FirstName = "Júlia", LastName = "Mangaba", HireDate = DateTime.Parse("2004-03-29") },
                new Employee { FirstName = "Carla", LastName = "Porto", HireDate = DateTime.Parse("2014-09-12") },
                new Employee { FirstName = "Jammily", LastName = "Pancratto", HireDate = DateTime.Parse("2014-09-12") },
                new Employee { FirstName = "Fabio", LastName = "Guerra", HireDate = DateTime.Parse("2004-03-29") }
            };

            foreach(Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "Enfermagem", Category = Category.NURSING },
                new Department { Name = "Medicina", Category = Category.MEDICINE }
            };

            foreach(Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var services = new Service[]
            {
                new Service { ServiceID = 1020, Title = "Consulta Pré-Natal Enfermagem", DepartmentID = departments.Single(s => s.Name == "Enfermagem").DepartmentID },
                new Service { ServiceID = 1025, Title = "Consulta Hipertenso-Diabético", DepartmentID = departments.Single(s => s.Name == "Medicina").DepartmentID },
                new Service { ServiceID = 1035, Title = "Consulta Puericultura", DepartmentID = departments.Single(s => s.Name == "Medicina").DepartmentID },
                new Service { ServiceID = 1040, Title = "Consulta Papanicolau", DepartmentID = departments.Single(s => s.Name == "Enfermagem").DepartmentID },
                new Service { ServiceID = 1045, Title = "Consulta Acupuntura", DepartmentID = departments.Single(s => s.Name == "Enfermagem").DepartmentID },
                new Service { ServiceID = 1050, Title = "Consulta Saúde Mental", DepartmentID = departments.Single(s => s.Name == "Medicina").DepartmentID },
                new Service { ServiceID = 1055, Title = "Visita Domiciliar Enfermagem", DepartmentID = departments.Single(s => s.Name == "Enfermagem").DepartmentID },

            };

            foreach(Service s in services)
            {
                context.Services.Add(s);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "César").ID,
                    Location = "Consultório 01" },
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "Correia").ID,
                    Location = "Consultório 02" },
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "Liula").ID,
                    Location = "Consultório 03" },
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "Mangaba").ID,
                    Location = "Consultório 04" },
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "Porto").ID,
                    Location = "Consultório 05" },
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "Pancratto").ID,
                    Location = "Consultório 06" },
                new OfficeAssignment {
                    EmployeeID = employees.Single( i => i.LastName == "Guerra").ID,
                    Location = "Consultório 07" },
            };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var serviceEmployees = new ServiceAssignment[]
            {
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Pré-Natal Enfermagem" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "César").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Puericultura" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "Pancratto").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Papanicolau" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "César").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Acupuntura" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "Pancratto").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Puericultura" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "Guerra").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Saúde Mental" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "Mangaba").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Saúde Mental" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "Mangaba").ID
                    },
                new ServiceAssignment {
                    ServiceID = services.Single(c => c.Title == "Consulta Acupuntura" ).ServiceID,
                    EmployeeID = employees.Single(i => i.LastName == "Guerra").ID
                    },
            };

            foreach (ServiceAssignment ci in serviceEmployees)
            {
                context.ServiceAssignments.Add(ci);
            }
            context.SaveChanges();

            var schedulings = new Schedule[]
            {
                new Schedule { ServiceID = 1020, PatientID = 1, Date = DateTime.Parse("2021-03-25"), Group = Group.PREGNANT },
                new Schedule { ServiceID = 1040, PatientID = 2, Date = DateTime.Parse("2021-03-25"), Group = Group.FERTIL_AGE_WOMEN },
                new Schedule { ServiceID = 1040, PatientID = 3, Date = DateTime.Parse("2021-03-25"), Group = Group.FERTIL_AGE_WOMEN },
                new Schedule { ServiceID = 1040, PatientID = 3, Date = DateTime.Parse("2021-03-25"), Group = Group.FERTIL_AGE_WOMEN },
                new Schedule { ServiceID = 1020, PatientID = 3, Date = DateTime.Parse("2021-04-25"), Group = Group.PREGNANT },
                new Schedule { ServiceID = 1020, PatientID = 3, Date = DateTime.Parse("2021-04-25"), Group = Group.PREGNANT },
                new Schedule { ServiceID = 1020, PatientID = 3, Date = DateTime.Parse("2021-04-29"), Group = Group.PREGNANT },
                new Schedule { ServiceID = 1050, PatientID = 3, Date = DateTime.Parse("2021-05-19"), Group = Group.FERTIL_AGE_WOMEN },
                new Schedule { ServiceID = 1050, PatientID = 3, Date = DateTime.Parse("2021-05-15"), Group = Group.FERTIL_AGE_WOMEN },
            };

            foreach(Schedule s in schedulings)
            {
                context.Schedules.Add(s);
            }
            context.SaveChanges();
        }
    }
}
