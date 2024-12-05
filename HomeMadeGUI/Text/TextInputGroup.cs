namespace HomeMadeGUI;

public class TextInputGroup
{

    public readonly List<TextInput> TextInputs = new List<TextInput>();
    
    public void AddInput(TextInput input)
    {
        TextInputs.Add(input);
        input.Group = this;
        input.GroupPosition = TextInputs.Count - 1;
    }

    public void HandleInputs()
    {
        foreach (var input in TextInputs)
        {
            input.HandleField();
        }
    }
}