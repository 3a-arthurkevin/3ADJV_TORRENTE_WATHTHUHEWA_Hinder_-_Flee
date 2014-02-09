//Pas attacher à un gameObject

public class PlayerDateClassScript
{ 

    public int networkPlayer;

    public int playerClass;


    public PlayerDateClassScript constructor()
    {
        PlayerDateClassScript player = new PlayerDateClassScript();

        player.networkPlayer = networkPlayer;
        player.playerClass = playerClass;

        return player;
    }
}
