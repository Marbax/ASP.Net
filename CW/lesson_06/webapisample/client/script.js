

let res = document.getElementById('result')

let url = 'http://localhost:49436/api/student'

fetch(url)
    .then(x => x.json())
        .then(x => {
            let html = ''

            for (let i = 0; i < x.length; i++) {
                html += `<tr><td>${x[i].Id}</td><td>${x[i].Name}</td><td>${x[i].Lastname}</td></tr>`
            }
          
            res.innerHTML = html
        });