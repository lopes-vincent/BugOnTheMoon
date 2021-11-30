class Arm part of Robot {
    everyFrame = {
        if (hit("Checkpoint", function (checkpointHitted))) {
            <color=#85C46C>// No, really ?</color>
            checkpointHitted.enable();
        }
    }
    
    function fire() {
        <color=#85C46C>// Todo implement something to fight against aliens</color>
    }
}
