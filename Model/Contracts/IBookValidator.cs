using Shared.DTOs;

namespace Model.Contracts;

public interface IBookValidator
{
    void Validate(BookDto book);
}
