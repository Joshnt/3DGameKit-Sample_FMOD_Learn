// FMOD Studio Script: Export all event names (no paths) to CSV
// Appears under Scripts > Export > Export Event Names

studio.menu.addMenuItem({
    name: "Export/Export Event Names to CSV",
    execute: function () {
        var events = [];

        function collectEvents(folder) {
            folder.items.forEach(function (item) {
                if (item.isOfType("Event")) {
                    events.push(item);
                } else if (item.isOfType("Folder")) {
                    collectEvents(item);
                }
            });
        }

        // Start from the root event folders
        studio.project.workspace.masterEventFolder.items.forEach(function (item) {
            if (item.isOfType("Event")) {
                events.push(item);
            } else if (item.isOfType("Folder")) {
                collectEvents(item);
            }
        });

        // Build CSV
        var csvLines = ["Event Name"];
        events.forEach(function (event) {
            csvLines.push('"' + event.name + '"');
        });

        // Save next to .fspro
        var filePath = studio.project.filePath.replace(/\.fspro$/, "_EventNames.csv");
        var file = studio.system.getFile(filePath);
        file.writeText(csvLines.join("\n"));
        file.close();

        studio.system.print("Exported " + events.length + " event names to " + filePath);
    }
});
