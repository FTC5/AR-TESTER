public interface IMenuSubcomponentsViewer
{
    enum ElementType
    {
        All,
        DefaultHidden,
        DefaultActive
    }
    void ActivateElements();
    void HideElements(ElementType type = ElementType.All);
}