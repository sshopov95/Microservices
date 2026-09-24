using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersApi.Models
{
    public class Vehicle
    {
        /// <summary>
        /// Нарочно изгасен A_I. Тук трябва да се съдържа ID-то на автомобила от другата БД.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
