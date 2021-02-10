using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchedulingPatients.MVC.Models
{
    public enum Genre
    {
        [Display(Name = "Masculino")]
        MASCULINO,
        [Display(Name = "Feminino")]
        FEMININO
    }

    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Nascimento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Sexo")]
        public Genre Genre { get; set; }

        public ICollection<Schedule> Schedules { get; set; }

        [Display(Name = "Nome Completo")]
        public string FullName
        {
            get
            {
                return $"{FirstName} ${LastName}";
            }
        }

        public int Idade
        {
            get
            {
                return CalculaIdade(this.BirthDate);
            }
        }

        private int CalculaIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if(DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }
    }
}
