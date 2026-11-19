// Run with mongosh. The database/collection names match appsettings.json.
// The script creates 100 items, 20 players, and one inventory per player.
const demoDb = db.getSiblingDB("ServerDB");
const now = new Date();

for (let i = 1; i <= 100; i++) {
  const suffix = String(i).padStart(3, "0");
  demoDb.Item.updateOne(
    { ItemName: `DEMO-Item-${suffix}` },
    {
      $setOnInsert: {
        CreatedDate: now,
        ModifiedDate: now,
        ItemName: `DEMO-Item-${suffix}`
      }
    },
    { upsert: true }
  );
}

const demoItems = demoDb.Item.find({ ItemName: /^DEMO-Item-/ }).sort({ ItemName: 1 }).toArray();

for (let i = 1; i <= 20; i++) {
  const suffix = String(i).padStart(3, "0");
  const playerName = `DEMO-Player-${suffix}`;
  demoDb.Player.updateOne(
    { PlayerName: playerName },
    {
      $setOnInsert: {
        CreatedDate: now,
        ModifiedDate: now,
        UserId: ObjectId(`000000000000000000000${suffix}`),
        PlayerName: playerName
      }
    },
    { upsert: true }
  );

  const player = demoDb.Player.findOne({ PlayerName: playerName });
  const slots = [0, 1, 2].map((offset) => {
    const item = demoItems[(i * 3 + offset) % demoItems.length];
    return {
      ItemId: item._id,
      Quantity: ((i + offset) % 9) + 1,
      CreatedDate: now,
      ModifiedDate: now
    };
  });

  demoDb.Inventory.updateOne(
    { PlayerId: player._id },
    {
      $setOnInsert: {
        CreatedDate: now,
        ModifiedDate: now,
        PlayerId: player._id,
        Items: slots
      }
    },
    { upsert: true }
  );
}

const demoPlayerIds = demoDb.Player.find({ PlayerName: /^DEMO-Player-/ }, { _id: 1 }).toArray().map((player) => player._id);

printjson({
  items: demoDb.Item.countDocuments({ ItemName: /^DEMO-Item-/ }),
  players: demoDb.Player.countDocuments({ PlayerName: /^DEMO-Player-/ }),
  inventories: demoDb.Inventory.countDocuments({ PlayerId: { $in: demoPlayerIds } })
});
