using Akka.Actor;
using StackExchange.Redis;

public class RedisActor : ReceiveActor
{
    private readonly IDatabase _redis;

    public RedisActor()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        _redis = redis.GetDatabase();

        /*
        ReceiveAsync<GetAndIncreaseLevel>(async msg =>
        {
            var nicknameKey = $"user:{msg.AccountId}:nickname";
            var levelKey = $"user:{msg.AccountId}:level";

            // 닉네임 없으면 새로 생성
            string nickname;
            var existingNickname = await _redis.StringGetAsync(nicknameKey);
            if (existingNickname.HasValue)
                nickname = existingNickname.ToString();
            else
            {
                nickname = "닉네임_" + Guid.NewGuid().ToString("N")[..6];
                await _redis.StringSetAsync(nicknameKey, nickname);
            }

            // 레벨 가져오기 및 증가
            var levelValue = await _redis.StringGetAsync(levelKey);
            int level = levelValue.HasValue ? int.Parse(levelValue) : 1;
            level++;

            await _redis.StringSetAsync(levelKey, level);

            Sender.Tell(new LevelResult(nickname, level));
        });*/
    }
}