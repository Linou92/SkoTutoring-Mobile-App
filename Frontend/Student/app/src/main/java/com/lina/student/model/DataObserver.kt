package com.lina.student.model

import io.reactivex.subjects.PublishSubject

class DataObserver {
    constructor(errorMessage: PublishSubject<String>, dataObject: PublishSubject<Any>) {
        this.errorMessage = errorMessage
        this.dataObject = dataObject
    }

    constructor()

    var errorMessage: PublishSubject<String> = PublishSubject.create()
    var dataObject: PublishSubject<Any> = PublishSubject.create()
}