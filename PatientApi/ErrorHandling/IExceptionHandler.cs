namespace PatientApi.ErrorHandling;

public interface IExceptionHandler
{
    void Handle(Exception exception);
}