namespace AntiDupl.NET.Core.Enums
{
    public enum LocalActionType //то же что и  enum adLocalActionType : adInt32, также еще в class HotKeyOptions enum Action
    {
        DeleteDefect = 0,
        DeleteFirst = 1,
        DeleteSecond = 2,
        DeleteBoth = 3,
        RenameFirstToSecond = 4,
        RenameSecondToFirst = 5,
        RenameFirstLikeSecond = 6,
        RenameSecondLikeFirst = 7,
        MoveFirstToSecond = 8,
        MoveSecondToFirst = 9,
        MoveAndRenameFirstToSecond = 10,
        MoveAndRenameSecondToFirst = 11,
        PerformHint = 12,
        Mistake = 13,
    }
}
