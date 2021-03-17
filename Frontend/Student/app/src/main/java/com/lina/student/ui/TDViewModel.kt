package com.lina.student.ui

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import io.reactivex.subjects.PublishSubject


open class TDViewModel : ViewModel() {
    private val currentFragment = MutableLiveData<Int>()
    val progressState = MutableLiveData<Boolean>()
    val finish = MutableLiveData<Boolean>()
    val errorMessage = PublishSubject.create<String>()
    val noData = MutableLiveData<Boolean>()


    fun changeCurrentFragment(i: Int) {
        currentFragment.postValue(i)
    }

    fun getCurrentFragment(): MutableLiveData<Int> {
        return currentFragment
    }

}