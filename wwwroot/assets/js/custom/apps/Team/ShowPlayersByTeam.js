function GetPlayers() {
    var id = $("#playerTeamId").val();
    console.log("$(this) :- ", $(this));
    window.location = "/Team/GetPlayers?teamId=" + id;
}
