public static class MoneyBehaviour {
    public static void UpdateMoney(Player player, int amount) {
        player.coins += amount;
        if(player.coins < 0)
            player.coins = 0;
    }

    public static void PlayerTransaction(Player receiver, Player giver, int amount) {
        UpdateMoney(receiver, amount);
        UpdateMoney(giver, -amount);

        if(giver.coins < 0)
            giver.coins = 0;
    }
}