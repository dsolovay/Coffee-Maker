# Coffee-Maker
Implementation of Bob Martin's Mark IV Coffee Maker exercise

To try this out, run console app.  Illustrates SOLID, and especially Interface Segregation Princieple.  Also uses a State Machine, which Uncle Bob strongly hinted was expected:

>What I expect of my students is a set of class diagrams, sequence diagrams, and state machines.  
— *Agile Principles, Patterns, and Practices in  C#*, Chapter 20, "Heuristics and Coffee"

Examples of ISP:
- Separation of API into ISenosrs and IControls, to ensure that SensorUpdater and ControlUpdater only depend on methods they use.  
- Generalization of active classes into IUpdater interface, with composite pattern.
- Hiding setter of StateMachine behind IStateProvider interface, so that interacting classes could not change it, through inadvertence or developer laziness. ("Hey, I could just set it to 'Ready' and go home.")  There's a nice symetry here: just as the hardware API is split into ISensors and IControls, the StateMachine is split into IEventReceiver and IStateProvider.  If you are going to access it, you are going to have a point of view.

The real ah-hah moment for me was how much nicer intellisense was when only the mehtods I cared about were visible. Just a nicer way to work with code.
