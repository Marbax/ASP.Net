let getAllBtn = document.querySelector('.get-all-goods')
let getByIdBtn = document.querySelector('.get-good-by-id')
let goodIdInput = document.querySelector('.good-id')
let tBody = document.querySelector('.tbody')

getAllBtn.addEventListener('click', async () => {
    tBody.innerHTML = ''
    let rowsArr = await GetAllGoods()
    rowsArr.forEach(element => {
        try {
            tBody.appendChild(element)
        } catch (error) {
        }
    });

})

getByIdBtn.addEventListener('click', async () => {
    tBody.innerHTML = ''
    let id = goodIdInput.value
    if (id > 0) {
        let row = await GetGoodById(id)
        tBody.appendChild(row)
    }
})

async function GetAllGoods() {
    try {
        let resp = await fetch('https://localhost:44395/api/v1/goods/')
        let jsonResp = await resp.json()
        let trows = []
        jsonResp.forEach(async (element) => {
            let row = await CreateRow(element)
            trows.push(row)
        });
        return trows
    } catch (error) {
        let r = document.createElement('tr')
        r.textContent = error
        return r
    }
}

async function GetGoodById(id) {
    try {
        let resp = await fetch(`https://localhost:44395/api/v1/goods/${id}`)
        let jsonResp = await resp.json()
        if (resp.status != 200) {
            throw new Error(jsonResp['Message'])
        }
        return await CreateRow(jsonResp)

    } catch (error) {
        let r = document.createElement('tr')
        r.classList.add('text-center')
        r.textContent = error
        return r
    }
}


async function CreateRow(element) {

    let row = document.createElement('tr')

    let tdId = document.createElement('td')
    tdId.textContent = element['GoodId']
    row.appendChild(tdId)

    let tdName = document.createElement('td')
    tdName.textContent = element['GoodName']
    row.appendChild(tdName)

    let tdCatId = document.createElement('td')
    tdCatId.textContent = element['CategoryId']
    row.appendChild(tdCatId)

    let tdManId = document.createElement('td')
    tdManId.textContent = element['ManufacturerId']
    row.appendChild(tdManId)

    let tdPrice = document.createElement('td')
    tdPrice.textContent = element['Price']
    row.appendChild(tdPrice)

    let tdGoodCount = document.createElement('td')
    tdGoodCount.textContent = element['GoodCount']
    row.appendChild(tdGoodCount)

    let photosArr = element['Photos']
    if (photosArr != null && photosArr.length > 0) {
        let imgDiv = document.createElement('div')
        imgDiv.classList.add('d-flex')
        photosArr.forEach(image => {
            let img = document.createElement('img')
            img.src = image
            img.width = '50px'
            img.classList.add('m-2')
            imgDiv.appendChild(img)
        });
        let trPhotos = document.createElement('tr')
        trPhotos.appendChild(imgDiv)
        row.appendChild(trPhotos)
    }
    return row
}