//Pas attacher à un gameObject

public class PlayerDateClassScript
{ 
    //Variable begin

    public int networkPlayer;

    public string playerName;

    public int playerClass;

    //Variable End

    public PlayerDateClassScript constructor()
    {
        PlayerDateClassScript player = new PlayerDateClassScript();

        player.networkPlayer = networkPlayer;
        player.playerName = playerName;
        player.playerClass = playerClass;

        return player;
    }
}
