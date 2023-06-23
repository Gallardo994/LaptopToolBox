namespace GHelper.Commands;

public interface ISTACommandLoop
{
    public void Enqueue(ISTACommand command);
}