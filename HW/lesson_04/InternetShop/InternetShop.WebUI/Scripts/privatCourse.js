
let privCont = document.getElementById('privatCourseContainer')

let getCourse = fetch('https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5')
    .then(res => res.json())
    .then((out) => {
        let usd = ''
        out.forEach(item => {
            let p = document.createElement('p')
            p.classList.add('m-0')
            if (item['ccy'].toLowerCase() === 'usd') {
                usd = { buy: parseFloat(item['buy']), sale: parseFloat(item['sale']) }
            }
            if (item['ccy'].toLowerCase() === 'btc') {
                p.innerHTML = `<b>${item['ccy']}</b>: ${(parseFloat(item['buy']) * usd.buy).toFixed(3)} - ${(parseFloat(item['sale']) * usd.sale).toFixed(3)}`
            }
            else {
                p.innerHTML = `<b>${item['ccy']}</b>: ${parseFloat(item['buy'])} - ${parseFloat(item['sale'])}`
            }

            privCont.appendChild(p)
        });
    })
    .catch(err => { throw err });