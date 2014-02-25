//Pas attacher à un gameObject

public class PlayerDateClassScript
{ 

    public int networkPlayer;

    public string playerName;

    public int playerClass;


    public PlayerDateClassScript constructor()
    {
        PlayerDateClassScript player = new PlayerDateClassScript();

        player.networkPlayer = networkPlayer;
        player.playerName = playerName;
        player.playerClass = playerClass;

        return player;
    }
}
