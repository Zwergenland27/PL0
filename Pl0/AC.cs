namespace Pl0;

public record AC
{
    public int FollowState;
    
    public AutomataAction Action;

    private AC(
        int followState,
        AutomataAction action)
    {
        FollowState = followState;
        Action = action;
    }
    
    public static AC R(int followState)
    {
        return new AC(followState, AutomataAction.Read);
    }
    
    public static AC E(int followState)
    {
        return new AC(followState, AutomataAction.Exit);
    }
    
    public static AC WUR(int followState)
    {
        return new AC(followState, AutomataAction.WriteUpperRead);
    }
    public static AC WR(int followState)
    {
        return new AC(followState, AutomataAction.WriteRead);
    }
    
    public static AC WRE(int followState)
    {
        return new AC(followState, AutomataAction.WriteReadExit);
    }
}