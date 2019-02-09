## TODO
- Shotgun should be pellet based
- Shotgun shell particles
- Improve Shotgun Particle effects
- Expose shotgun damage
- Create a better damage model (Damage model abstract class?)
- Improve enemy manager

# Maestro

An RPG game in which the player must utilize music, rhythm, and composition to defeat enemies and solve puzzles.

## Design Blog Jan 13th - Jan 27th, 2019

##### The Idea

Being the first week that our team started working on Maestro, things were very hectic. At the game jam we wanted to create a jrpg where you attack by doing guitar hero riffs. We prototyped out note hitting and detection at the game jam but found the system lacking. It felt like music was just tacked on as an afterthought, it wasn't _integrated_ properly.

After some soul searching and ideation we came up with a new and better idea before our first team meeting which happened sometime in the middle of that week (I think). Our game idea is heavily inspired off of existing rhythm games like 140 and Crypt of the Necrodancer. In these games the music isn't just an add in that plays the background, it wouldn't be wrong to say that the game would not function without the music, or that they are inseperable. We strove to create a game like this, one where the music and the game were intertwined with eachother.

We decided to create a top down room-clearing action game much in the spirit of Binding of Isaac and Enter the Gungeon. We were really inspired by how 140 uses it's game objects and mechanics to _create_ music. So we wanted to make a game were the music was driven by the gameplay, as opposed to the other way around as in Crypt of the Necrodancer. To this end our game makes use of several mechanics

- The actions of all game objects are to be constrained to happen at rhythmic intervals
- The input that the player gives for each action must be given in time with a predetermined rhythm

![Image Failed To Load](https://raw.githubusercontent.com/FreakingBarbarians/FreakingBarbarians_Images/master/MaestroGame/UIExample.PNG "What our game could possibly look like")


So you get things like enemies moving to a rhythm, creating a beat. Or moving walls shifting every downbeat. And the player must tap out a rhythm on his/her controller in order to perform actions. If we wrap this around a background beat for each level and the general paradigm of topdown action shooters then we have a symphony of different game objects all acting in harmony. Or at least that's the goal.

`To create music by interacting with the world`

It would not be wrong to say that this is the core theme behind our game.

The look and feel was decided by our artists Denzel and Kristan. We wanted to go for a bright and vibrant look.

![Image Failed To Load](https://raw.githubusercontent.com/FreakingBarbarians/FreakingBarbarians_Images/master/MaestroGame/MoodBoard.PNG "Our Moodboard")

---

##### The Pitch

We had our first meeting where we divvied up tasks, talked about and refined ideas, as well as came up with a general look and feel goal for our game expressed as a mood-board. During this time we decided what we would present for the pitch. We needed to make sure that the audience really understood the core concept behind our game.

We decided that three of us would be presenting the pitch, Ray, Thommy and Kristan. Thommy would go first, presenting the mechanics and overall idea behind the game, Kristan would then go over the look and feel of the game, and Ray would go over an example of music and gameplay integration.

We were all really nervous when we went up to present but the delivery of the pitch was excellent. One of the judges said he liked it best.

We also got feedback, criticisms and ideas that we hadn't considered before.

- Potentially a lot going on at once, keeping track of enemy movement/bullets, your own character's movements, as well as item rhythms. Could overwhelm the player.
    - To this end we decided that we would remove the need for players to aim their weapons. And we are also considering removing player movement entirely and going for a tower-defense or on rails concept but we are still undecided.
    - The main issue is that the player might find it very difficult to move along numerous axis as they play music as they fight enemies, etc. Too much to deal with all at once. 
- Large amounts of enemies could drown out the player's sounds. The music could end up being almost entirely enemy-based, while player sounds are just background noise.
    - This is something to be considered heavily in sound design. We should make the player actions iconic, the melody, or the main theme of each song. Failing that a system that ensures that the player's music is the most important.
    - The judge's concern was that control is in the hands of the enemy, not the player. If the enemy or the computer-controlled side is mostly in control of the melody, this means that they dictate how things're going to go, not the player.
- On that note, too many enemies at once would likely make the game too loud, also could result in a blurb of noise rather than smooth, coordinated music.
    - We need a system that ensures volume is kept at a respectable level, and game objects create sound harmoniously.
- Consider looking at the game "Disco is Dead", a previous Level Up winner.
    - Considered
- Keep in mind the idea of using peripheral devices (notably a guitar hero/rock band guitar).
    - We like this idea but we would have to redisgn a large part of our game, we are still undecided. 
- The Level Up convention is likely going to be very loud - think of ways to tackle this (Maybe bring noise-canceling headphones?)
    - Ray will borrow his mom's noise cancelling headphones. (Thanks mom)
- Try to stay consistent with a theme. It won't look good if you have this really vibrant game playing rock music based in a sci-fi age, or some other strange combination.
    - Music designers need to be coordinated with artists
    - They liked the aesthetic but they believe we need to "tighten" it more.
- They reminded us that gameplay can change at the prototype. We may discover that some things work better or don't work at all at the prototype stage.

Keeping these things in mind we are eager and excited to build the next part of our game. We hope to get something minimally playable by Tuesday January 29th.

Here's a video link to our tech demo.

https://youtu.be/hmdybQniNog

And here's another link because I forgot something in the first one. Oh well, watch both if you want.

https://youtu.be/CgVh9DTWrUQ

If the video is unavailable shoot me an email at `paarthurnax@live.ca` There's probably something wrong with the publicity settings.

## Design Blog Jan 28th - Feb 3rd, 2019

##### Tech Demo

In case you missed it, the following two videos below demonstrate some tech demos as to what our game might feel like in terms of mechanics.

https://youtu.be/hmdybQniNog

Once again, this second video includes some things accidently forgotten from the previous video.

https://youtu.be/CgVh9DTWrUQ

##### The Meeting

This week, we focused mostly on design and aesthetics. The team wants to settle on one central theme, colour palette, etc. so that we can have a clearer goal in mind as to what we want the final product to look like. The team had a meeting on Thursday to discuss this more in detail.

The first thing that the team agreed on was the central theme of the game. We have already decided to stick with the colour palette demonstrated in the previous blog post. The team decided that a sci-fi theme would be very fitting with this palette.

Prior to this meeting, team member Meena created a character which could be used for the protagonist of the game:
![Image Failed To Load](https://cdn.discordapp.com/attachments/538651386951368714/539623267304996884/received_2035285553216494.png "Initial protagonist character art")

The team liked how the character looked. However, during the meeting, we came to the consensus that the chibi-style character would not be a good fit in our game and opted for a more human-like character. One such character that we may be looking at for reference is the character Lea from the game CrossCode, shown below.
![Image Failed To Load](https://gamepedia.cursecdn.com/crosscode_gamepedia/thumb/1/12/Lea.png/508px-Lea.png "Lea from CrossCode")

We also looked at another game that we can take inspiration from: Furi. Furi has a colour palette similar to what the team has in mind, as well as a similar sci-fi theme going on too.

A few other things that the team went over during the meeting are:
- Each level will consist of 5-6 rooms, with a boss room and an elevator at the end. The player's goal is the reach this elevator.
    - Elevators would act as level transitions, displaying the results of the previous floor, and maybe healing the player as well.
- General plot: An evil corporation has stolen/taken over all of the music industry, the player wants to stop their ways
    - There is a bit more to it (Why the player is even doing this), but then we would be spoiling the plot!
    - Each level would be a different floor of the corporation's building (Which is currently a massive space ship), such as a factory floor, cafeteria, etc.
- Due to sheer difficulty, the team has decided that the player will only have to worry about two rhythms, as opposed to the previously mentioned 4 rhythms. Trying to manage 4 potentially unrelated rhythms is simply not feasible for any one person.
- Give the maestro bar more purpose. Perhaps have a "use" item which consumes a certain amount of the meter to activate an effect, such as a shield.
- Likely going to use pixel-esque textures for rooms. Drawing inspiration from Octopath Traveler, shown below
![Image Failed To Load](https://d2skuhm0vrry40.cloudfront.net/2018/articles/2018-07-12-09-18/2018071115202500_93C1C73A3BAF9123A15B9B24886B634B.jpg/EG11/resize/690x-1/quality/75/format/jpg "Octopath Traveler screenshot")

##### After The Meeting

The team has made a few more advancements since we met on Thursday. Denzel has created something initially as a joke, but actually has the potential to be an NPC or enemy in this game:

![Image Failed To Load](https://cdn.pbrd.co/images/HZvcOeV.png "Denzel's dancing jukebox")

Kristan created three example lobby layouts to give an idea of what rooms in the game might look like.
![Image Failed To Load](https://media.discordapp.net/attachments/538651386951368714/541344852013744168/lobby_level_roughs.jpg?width=789&height=607 "Lobby layout sketch")

Alongside this, Denzel has also created a basic block-layout of said room:
![Image Failed To Load](https://media.discordapp.net/attachments/538651386951368714/541523611077574676/basic_room.PNG?width=963&height=608 "Basic block layout of the room")

Meena has also created more sketches of what the protagonist could look like:
![Image Failed To Load](https://cdn.discordapp.com/attachments/538651386951368714/541722699819647011/20190203_154910.jpg "Character concept 1")
![Image Failed To Load](https://cdn.discordapp.com/attachments/538651386951368714/541722699819647019/20190203_154943.jpg "Character concept 2")

Our music team member Paul has also created some tracks for attacks, background loops, and so on. Unfortunately, I have no means of uploading such files to the blog (Should I be able to, I will update this section, and add the files).

Overall, our team is making good progress towards the next milestone, however we are also aware that there is still much to be done before the final product is complete and ready for launch.

## Design Blog Feb 3rd, 2019 - Feb 11, 2019

##### Design Presentation & Design Document
We had a presentation to another series of panelists this week. This time, it was one of the most important stages in development. When a game studio's producing a game for another group, they must showcase beyond their idea. The best ideas can have poor implementation. The Design Presentantion goes deep into technical description, in as much detail as it can, to describe every aspect. In the 5-10 minutes given, we had to explain it all. Our core gameplay, the mechanics, what goals are achieved in gameplay, a sample of the music, level design and samples of levels, the visual design and aesthetic, the list goes on. 

Slides we used: https://docs.google.com/presentation/d/1nw-467-qcknn7Q0F5kbEFkOCfnJglWwER3KLiApI-Sc/edit#slide=id.g4ebe9dad3f_0_155

In the end, the group (or the members who were there) felt we presented things adequately but with various areas of improvement for presentations. 

The feedback obtained, as with all the feedback these experts give us, was very valuable. 
- As music is our game's focus, rhythm game after all, music should be front & center in our presentations. It should be explained very soon.
-Player interaction is also important to explain
-

##### The Meeting
In spite of many members being too busy or the location of our meeting too inconvenient to attend (About only 4 of our members attended. For their privacy, I won't say who.) we still got progress. Throughout the week, there were classes cancelled at U of T & even alarms at OCAD for severe weather. Therefore, our previous meeting place was unusable. Next, many of our members had their own assignments, tests, etc due thus they couldn't make it. We kept them updated post-meeting on Discord. They agreed with the decisions.

In our discussion, we considered a shift away from shooting & weaponry & towards more puzzles. While we considered doing both rhythm based combat & puzzles, we encountered difficulties balancing them without risks of overwhelming the player. Within our Design Presentation & even presentations before, these were concerns from the panelists of different presentations. We've decided to go with rhythm puzzles more than combat, scrapping weapons in favor of the puzzles we put. 


