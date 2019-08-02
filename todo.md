#TODOS

Introduce version in ifilecomponent load
Fix toolnumber size - now panelcomponents save as byte and tool saves as int - set all to int
Do program stop on tool change (cnc does not stop on tool change)
Continue fatline implementation (fatline corners)


Tool change - allow splitting tool passes into separate files
Tool change - simulate with:

  1. raise, stop spindle, move to toolchange position
  2. M0 (await unpause)
  3. probe, set new z0, raise
  4. restore position, Continue

Make new spoil jig for panels
Mill rectangular pocket into MDF. Peck drill 0,0 corner to allow sharp panel corner
optional - mill pocket for probe. Use old button cell battery?

Separate(slower) Z speed, especially for thin tools. They tend to vibrate on too quick z speed making bumps when starting a line.
Start with halving milling speed, then perhaps allow user set z speed?

Try to optimize lettering. Avoid raising the drill bit if possible. How?

Try to optimize milling order.
One option is to start with the item closest to 0,0 and select the closest one when milling has ended.
Some screwups can happen, for instance you can "wander off" and then have to travel far to get to the forgotten one.
Another option is to increase by the longest axis and mill by increasing small axis.
