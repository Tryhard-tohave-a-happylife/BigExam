function search() {
    var area = document.getElementById("dia-diem").value;
    var job = document.getElementById("linh-vuc").value;
    var name = document.getElementById("key-word").value;
    console.log(name);
    var dbParam = "Name=" + name + "&AreaID=" + area + "&JobID=" + job;
    window.location.href = "/Employee/SearchCandidateResult? " + dbParam;
}
function refresh() {
    if (document.getElementById("linh-vuc").value != 0 || document.getElementById("dia-diem").value != 0 || document.getElementById("key-word").value) {
        console.log(document.getElementById("key-word").value);
        window.location.href = "/SearchCandidateResult";
        document.getElementById("linh-vuc").value = 0;
        document.getElementById("dia-diem").value = 0;
        document.getElementById("key-word").value = "";

    }
}
