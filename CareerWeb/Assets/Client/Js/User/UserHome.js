
function search() {
    var area = document.getElementById("dia-diem").value;
    var job = document.getElementById("linh-vuc").value;
    var name = document.getElementById("key-word").value;
    console.log(name);
    var dbParam = "Name=" + name + "&AreaID=" + area + "&JobID=" + job;
    window.location.href = "/Employee/SearchCandidateResult?" + dbParam;
}