using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P1A_P1_AngelGuerrero.Models;

public class Registro
{
    [Key]
    public int RegistroId { get; set; }

}
