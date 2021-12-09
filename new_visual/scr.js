let yyy = []

let edd = []

let options = {};

let container = document.getElementById('mynetwork');

function get_nums() {
    let numNodes = document.getElementById('numNodes').value;
    return numNodes;
}

const btnStart = document.getElementById('startButton');
const btnAddEdge = document.getElementById('addEdge');
const btnClear = document.getElementById('clear');
const btnDelNode = document.getElementById('delNodeBtn');
const btnDelEdge = document.getElementById('delEdgeBtn');
const btnDeicstra = document.getElementById('deicstra');
const text = document.getElementById('relationOfCounts');
const btnDefault = document.getElementById('default');

btnStart.onclick = function () {
    let num = get_nums();

    let name = document.getElementById('nameNodes').value;
    if (document.getElementById('nameNodes').value == "") {
        name = num;
    }

    yyy = yyy.concat({ id: num, label: name + ", id =" + num });

    for (let i = 0; i < getLenYYY(); i++) {
        createTable(yyy[i]['id']);
    }

    draw();
}



btnAddEdge.onclick = function () {
    let fromq = document.getElementById('fromEdge').value;
    let toq = document.getElementById('toEdge').value;
    let weight = document.getElementById('Weight').value;

    edd = edd.concat({ from: fromq, to: toq, label: weight, font: { align: "horizontal" }, arrows: "to" });

    for (let i = 0; i < getLenYYY(); i++) {
        createTable(yyy[i]['id']);
    }

    draw();
}

btnClear.onclick = function () {
    window.location.reload();
}

btnDelNode.onclick = function () {
    let num = get_nums();

    let temp = [];
    for (let i = 0; i < yyy.length; i += 1) {
        if (yyy[i]['id'] != num) {
            temp = temp.concat(yyy[i]);
        }
    }
    yyy = temp;

    let tempEdges = [];
    for (let i = 0; i < edd.length; i += 1) {
        if (edd[i]['from'] != num && edd[i]['to'] != num) {
            tempEdges = tempEdges.concat(edd[i]);
        }
    }
    edd = tempEdges;

    for (let i = 0; i < getLenYYY(); i++) {
        createTable(yyy[i]['id']);
    }

    draw();
}

btnDelEdge.onclick = function () {
    let fromq = document.getElementById('fromEdge').value;
    let toq = document.getElementById('toEdge').value;

    let tempEdges = [];
    for (let i = 0; i < edd.length; i += 1) {
        if (edd[i]['from'] != fromq || edd[i]['to'] != toq) {
            tempEdges = tempEdges.concat(edd[i]);
        }
    }
    edd = tempEdges;

    for (let i = 0; i < getLenYYY(); i++) {
        createTable(yyy[i]['id']);
    }

    draw();
}

function Dijkstra(matrix, start = 0) {
    const rows = matrix.length,
        cols = matrix[0].length;

    // инициализируем расстояние
    const distance = new Array(rows).fill(Infinity);
    distance[start] = 0;

    for (let i = 0; i < rows; i++) {
        // Недоступные вершины не могут быть использованы в качестве транзитных точек перехода
        if (distance[i] < Infinity) {
            for (let j = 0; j < cols; j++) {
                // Например, сравнивая размер расстояния [i] + матрица [i] [j] и расстояние [j], чтобы определить, обновлять ли расстояние [j].
                if (matrix[i][j] + distance[i] < distance[j]) {
                    distance[j] = matrix[i][j] + distance[i];
                }
            }
        }
    }

    const table = document.getElementById('deicstraTable');

    if (table.rows.length > 0) {
        for (i = table.tBodies[0].rows.length - 1; i >= 0; i--) { table.tBodies[0].deleteRow(i); }
    }

    console.log(distance.length);
    let rowHeader = table.insertRow();
    for (let prop in distance) {
        let cell = rowHeader.insertCell();
        cell.innerText = yyy[prop]['label'];
    }

    rowHeader = table.insertRow();
    for (let prop in distance) {
        let cell = rowHeader.insertCell();
        cell.innerText = distance[prop];
    }

    return distance;
}

/**
* Матрица смежности
* Значение - это вес ребра между вершиной и вершиной, 0 означает отсутствие самоконтроля, большое число означает отсутствие ребра (например, 10000)
* */
const MAX_INTEGER = Infinity; // Нет ребер или недоступен в ориентированном графе
const MIN_INTEGER = 0; // Нет самоконтроля

btnDeicstra.onclick = function () {
    let num = get_nums();

    let temp = [];
    temp[0] = [];

    let arrLength = getLenYYY();

    let idFindNode;
    for (let i = 0; i < arrLength; i += 1) {
        if (yyy[i]['id'] == parseInt(num)) {
            idFindNode = i;
        }
    }

    let edgLength = getLenEDD();

    for (let i = 0; i < arrLength; i += 1) {
        temp[i] = [];
    }

    for (let i = 0; i < arrLength; i += 1) {
        for (let j = 0; j < arrLength; j += 1) {
            temp[i][j] = MAX_INTEGER;
        }
        temp[i][i] = MIN_INTEGER;
    }

    for (let q = 0; q < edgLength; q += 1) {
        let t1 = edd[q]['from'];
        let t2 = edd[q]['to'];
        for (let i = 0; i < arrLength; i += 1) {
            for (let j = 0; j < arrLength; j += 1) {
                if (yyy[i]['id'] == t1 && yyy[j]['id'] == t2) {
                    temp[i][j] = parseInt(edd[q]['label']);
                }

            }
        }
    }

    for (let i = 0; i < getLenYYY(); i++) {
        createTable(yyy[i]['id']);
    }

    if (num == "") {
        text.innerHTML = `Длины кратчайших путей от вершины 1 (по дефолту)`;

    }
    else {
        text.innerHTML = `Длины кратчайших путей от вершины ${num}`;
    }

    console.log(Dijkstra(temp, idFindNode));
}

btnDefault.onclick = function () {
    yyy = [{ id: 1, label: "Node 1, id =" + 1 },
    { id: 2, label: "Node 2, id =" + 2 },
    { id: 3, label: "Node 3, id =" + 3 },
    { id: 4, label: "Node 4, id =" + 4 },
    { id: 5, label: "Node 5, id =" + 5 }]

    edd = [{ from: 1, to: 2, label: "7", font: { align: "horizontal" }, arrows: "to" },
    { from: 1, to: 3, label: "3", font: { align: "horizontal" }, arrows: "to" },
    { from: 2, to: 4, label: "5", font: { align: "horizontal" }, arrows: "to" },
    { from: 2, to: 5, label: "9", font: { align: "horizontal" }, arrows: "to" },
    { from: 3, to: 4, label: "1", font: { align: "horizontal" }, arrows: "to" }]

    for (let i = 0; i < getLenYYY(); i++) {
        createTable(yyy[i]['id']);
    }

    draw();

}

function getLenYYY() {
    let arrLength = 0;
    yyy.forEach(function () {
        arrLength++
    })

    return arrLength;
}

function getLenEDD() {
    let edgLength = 0;
    edd.forEach(function () {
        edgLength++
    })

    return edgLength;
}

function createTable(num) {
    let temp = [];
    temp[0] = [];

    let arrLength = getLenYYY();

    let idFindNode;
    for (let i = 0; i < arrLength; i += 1) {
        if (yyy[i]['id'] == parseInt(num)) {
            idFindNode = i;
        }
    }

    let edgLength = getLenEDD();

    for (let i = 0; i < arrLength; i += 1) {
        temp[i] = [];
    }

    for (let i = 0; i < arrLength; i += 1) {
        for (let j = 0; j < arrLength; j += 1) {
            temp[i][j] = MAX_INTEGER;
        }
        temp[i][i] = MIN_INTEGER;
    }

    for (let q = 0; q < edgLength; q += 1) {
        let t1 = edd[q]['from'];
        let t2 = edd[q]['to'];
        for (let i = 0; i < arrLength; i += 1) {
            for (let j = 0; j < arrLength; j += 1) {
                if (yyy[i]['id'] == t1 && yyy[j]['id'] == t2) {
                    temp[i][j] = parseInt(edd[q]['label']);
                }

            }
        }
    }

    const table = document.getElementById('table');

    if (table.rows.length > 0) {
        for (i = table.tBodies[0].rows.length - 1; i >= 0; i--) { table.tBodies[0].deleteRow(i); }
    }

    let rowHeader = table.insertRow();
    let cell = rowHeader.insertCell(-1);
    cell.innerHTML = " ";
    for (let prop in temp[0]) {
        let cell = rowHeader.insertCell();
        cell.innerText = yyy[prop]['label'];
        console.log(yyy[prop]['label']);
    }

    for (let i = 0; i < temp.length; i++) {
        let row = table.insertRow();
        let cell = row.insertCell(-1);
        cell.innerHTML = yyy[i]['label'];
        for (let prop in temp[i]) {
            let cell = row.insertCell();

            cell.innerText = temp[i][prop];

        }
    }
}

function draw() {
    let nodes = new vis.DataSet(yyy
    );

    let edges = new vis.DataSet(edd
    );

    let data = {
        nodes: nodes,
        edges: edges
    };

    let network = new vis.Network(container, data, options);
}