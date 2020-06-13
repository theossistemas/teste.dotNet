using System;
using System.ComponentModel.DataAnnotations;

namespace MMM.Library.Infra.CrossCutting.Identity.Authorization
{
    public enum ESystemPermissions : short
    {
        NotSet = 0, // error

        // TODO: set enum for Claims
        [Display(GroupName = "Book", Name = "Read", Description = "Pode ler Livors")]
        BookRead = 10,
        [Display(GroupName = "Book", Name = "Update", Description = "Pode remver Livros")]
        BookEdit = 11,
        [Display(GroupName = "Book", Name = "Add", Description = "Pode Adicionar Livros")]
        BookAddNew = 12,
        [Display(GroupName = "Book", Name = "Remove", Description = "Pode remver Livros")]
        BookRemove = 13,        

        [Display(GroupName = "SuperAdmin", Name = "Accesso Completo", Description = "Super Usuário - Acesso em todos os recuros do Sistema")]
        AccessAll = Int16.MaxValue,
    }
}
