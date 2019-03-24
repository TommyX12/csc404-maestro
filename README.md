# Maestro

An RPG game in which the player must utilize music, and rhythm to defeat enemies.


Alpha video

https://www.youtube.com/watch?v=R6WamuxwXkM&feature=youtu.be

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
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/539623267304996884/received_2035285553216494.png "Initial protagonist character art")

The team liked how the character looked. However, during the meeting, we came to the consensus that the chibi-style character would not be a good fit in our game and opted for a more human-like character. One such character that we may be looking at for reference is the character Lea from the game CrossCode, shown below.
![ScreenShot](https://gamepedia.cursecdn.com/crosscode_gamepedia/thumb/1/12/Lea.png/508px-Lea.png "Lea from CrossCode")

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
We had a presentation to another series of panelists this week.  When a game studio's producing a game for another group, they must showcase beyond their idea. The best ideas can have poor implementation. The Design Presentantion goes deep into technical description, in as much detail as it can, to describe every aspect. In the 5-10 minutes given, we had to explain it all. Our core gameplay, the mechanics, what goals are achieved in gameplay, a sample of the music, level design and samples of levels, the visual design and aesthetic, the list goes on. 

Slides we used: 
https://docs.google.com/presentation/d/1nw-467-qcknn7Q0F5kbEFkOCfnJglWwER3KLiApI-Sc/edit#slide=id.g4ebe9dad3f_0_155

In the end, the group (or the members who were there) felt we presented things adequately but with various areas of improvement for presentations. 

The feedback obtained, as with all the feedback these experts give us, was very valuable. 
- As music is our game's focus, rhythm game after all, music should be front & center in our presentations. It should be explained very soon.
- Player interaction is also important to explain
- More clarity with how the music & rhythm will work i.e. time synchs
- How is it rhythm or how does it use rhythm? (Indeed, even from the start we're more like a dungeon crawler that uses rhythm & rhythm aspects than all-rhythm game like Elite Beat Agents or Theatrhythm Final Fantasy. )
- They recommended we check out Sound Shapes & Everyday Shooter. Some of our group are already familiar with these games.
![ScreenShot](https://media.playstation.com/is/image/SCEA/sound-shapes-screenshots-01-ps4-us-20mar15?$MediaCarousel_Original$)
![ScreenShot](https://www.everydayshooter.com/EverydayShooter_1.jpg)
- It's hard to balance both shooter, puzzle & rhythm at the same time.
- How's sound triggered? (Current answer: By the player, enemies & actions performed in the environment. A more detailed answer is still in progress.)
- If iteraction is with the tempo, consider visual cues. Not everyone is super keen on audio cues. For rhythm games to be more accessible to a general audience, they'll need to see cues too.
- Biggest thing: Figure out the rhythm mechanic
- How's it all going to be integrated?
- How do people interact with the tempo? 
- ~~When is Half-Life 3 coming out?~~

All of these concerns are valuable ones we plan to keep to heart & fully answer. ~~Especially that last one~~

As for the doc, it was one of the most important stages in development. We want to ensure our reader knows everything. We want the professor & whoever's reading to know everything It's on its way as of this writing, nearly there.

https://docs.google.com/document/d/1pzBNcJ1X8DKWTHz2AxP3mD6JzXRbEpm7DsJseca8l2c/edit#


##### The Meeting
In spite of many members being too busy or the location of our meeting too inconvenient to attend (About only 4 of our members attended. For the privacy of all those in the group, who came or didn't come won't be revealed.) we still got progress. Throughout the week, there were classes cancelled at U of T & even alarms at OCAD for severe weather. Therefore, our previous meeting place was unusable. Next, many of our members had their own assignments, tests, etc due thus they couldn't make it. We kept them updated post-meeting on Discord. They agreed with the decisions.

In our discussion, we considered a shift away from shooting & weaponry & towards more puzzles. While we considered doing both rhythm based combat & puzzles, we encountered difficulties balancing them without risks of overwhelming the player. As mentioned before, these were concerns from the panelists of different presentations, not just this one. We've decided to go with rhythm puzzles more than combat, scrapping weapons in favor of the puzzles we put.

In fact, we're considering a few more shifts from where were originally. One concern we all have is regarding fun. **How can we make it fun?** A few of our members presented playable demos to friends. The feedback was that it wasn't very fun to play. This is a pressing issue we plan to solve. It's the **how** that's the biggest problem.

Another issue is the puzzles themselves. Good puzzles don't grow on trees. (Or do they?) We could theoretically have the best base game yet poor puzzles, poor level design, etc can bring everything down. ~~That Ubisoft game Breath of the Wild suffers from this.~~ We do have volunteers in the group to create fun puzzles but if we focus on puzzles, their quality is a make or break thing.

On a side note, it was Whopper Wednesday thus the members who attended got Burger King. Between the bad weather, the tests coming up & everything, I suppose the members who arrived deserve something special.

##### New pictures, assets & more since last time

We had some new art & models since last time.

Floaty ball activator - 1
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542849995343396864/20190206_182918.jpg)
Button activator - 2
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542850156266127360/20190206_182926.jpg)
Radio activator - 1
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542850405781078020/20190206_182931.jpg)
Sequence activator
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542850499234234368/20190206_182941.jpg)
Protagonist Linework
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542882165562540043/Protagonist_Linework.png)
Ship exterior
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542994126967275520/shipexteriorx.jpg)
Roughs of enemies
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/542992897763442689/enemyroughs.jpg)
![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/543982712101535755/image0.jpg)

## Design Blog Feb 12th, 2019 - Feb 17th, 2019

The entry for this week can be found by following the link below:
https://docs.google.com/document/d/1-amUrab4W4mbpkweFuZ-aDp3PD5DjyAHpxu0krUbTKk/edit

## Demo 1

https://www.youtube.com/watch?v=NzPD-uGhl2U&feature=youtu.be

## Design Doc

https://docs.google.com/document/d/1pzBNcJ1X8DKWTHz2AxP3mD6JzXRbEpm7DsJseca8l2c/edit#

## Design Blog March 19th, 2019 - March 22th, 2019

##### Beta Demo Presentation

Thursday, March 21 was also the final presentation. For some inexplicable reason, a large number of our members were sick and couldn't make it. For their privacy, they will be kept anonymous. The largest impact this had on the presentation was that the computer used was another group member's computer meant to be a back-up to the back-ups. This back-up laptop was not a gaming laptop but played the game adequately. But because this was a back-up computer that was intended as a last resort compared to the gaming laptops (and back-up gaming laptops) of numerous members, it wasn't calibrated the best. The game is far from unplayable in this state but it required some extra calibration during the presentation. 

In this demo, the panellists stepped up to personally play our games. This game was intended to be played on a controller (Xbox) but we were forced to use the keyboard. Fortunately, the controls were only arrow keys for movement and a shooting button. It could practically be played using an Atari 2600 controller.

![ScreenShot](https://cdn.discordapp.com/attachments/536218953987653674/559230510010859521/41jDq2B8tYEL.png)

Thus, the difference exists but isn't too damaging. Our panellist didn't have much trouble picking up the game but the technical difficulties during the presentation had some small degree of interference.

Reception was overall positive. The panellists liked the neon-80s feel. Much of the positive feedback is similar to positive feedback we've obtained before so there's not much to say in that area without repeating. They didn't have any super-major gamebreaking gripes. 
What's new and worth mentioning are the parts they disliked and next steps for improvement:
- The laptop used wasn't calibrated the best and the tempo was a bit slow.
- They want to be able to tap to the beat even with their eyes closed.
- The calibration screen's music has multiple beats, unsure which beat to tap to.
- Beat should change.
- The line around the circle to indicate when to shoot was a bit confusing. Consider not having a line do the moving.
- Recommendation was something alike the Windows Loading circle.

![ScreenShot](https://i.gifer.com/BA5o.gif)

- The aiming lines may be unneccessary. 
- Make the music have strong beats which match the visuals, should be able to close your eyes and tap to the beat
- One found the "Don't get hit to regenerate health" to be awkward and unnecessary
- Something more apparent that your health is going down i.e. Call of Duty making the screen bloody (or covered in strawberry jam) or the music changing upon hit (we chose to make the music fuzz a bit)

![ScreenShot](https://aspiringgameprogrammer.files.wordpress.com/2009/11/vlcsnap-112634.png)

- The player's eyes are mostly on the spinning wheel, never looking down on the health bar. Consider putting health bar on the firing wheel.
- It's hard to see the enemies as they're watching the spinning wheel.

As for things to fix for two weeks from now:

- The enemy fire is hard to see, especially when focusing on the beat. Environment is a bit noisy.
- Have an indication before they shoot. 
- Be sure to nail the audio at Level-Up (We plan on getting headphones)
- Bullet Hell games often have colorful projectiles on a dark background, thus it's easier to see the projectiles. They're the brightest things so they're the most noticable.

![ScreenShot](https://vignette.wikia.nocookie.net/touhou/images/6/68/Th08screenLayout.jpg/revision/latest?cb=20051028013614)

- We have colors everywhere. We should try to have most of the brighter colors in the background, with a darker foreground so the bright projectiles are easily seen.
- Differentiate interactables with non-interactables visually. Make the interactables stand out. Make bullets brighter so they're more visible.
- The targetting lock-on circle isn't needed. The red circle may or may not be necessary. Possibly shrink or remove it. 
- Have a leveling system or special weapons so the player feels dominant.
- Consider adding optional objectives.

##### New pictures, assets & more since last time

![ScreenShot](https://cdn.discordapp.com/attachments/538651386951368714/555963430058721280/PlayArea1.png)
Arcade-themed level idea

![ScreenShot](https://cdn.discordapp.com/attachments/538107981972242432/557707592877604864/20190319_192557_1.mp4)
New effects

![ScreenShot](https://media.discordapp.net/attachments/538651386951368714/559132272373006355/20190323_173940.jpg?width=745&height=559)
Golden Disco ball